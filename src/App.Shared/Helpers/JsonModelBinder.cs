using System;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

public sealed class JsonModelBinder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        if (bindingContext == null) throw new ArgumentNullException(nameof(bindingContext));

        var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
        if (valueProviderResult == ValueProviderResult.None)
            return Task.CompletedTask;

        bindingContext.ModelState.SetModelValue(bindingContext.ModelName, valueProviderResult);

        var raw = valueProviderResult.FirstValue;
        if (string.IsNullOrWhiteSpace(raw))
        {
            bindingContext.Result = ModelBindingResult.Success(null);
            return Task.CompletedTask;
        }

        try
        {
            var result = JsonSerializer.Deserialize(raw, bindingContext.ModelType);
            bindingContext.Result = ModelBindingResult.Success(result);
        }
        catch (JsonException ex)
        {
            bindingContext.ModelState.TryAddModelError(bindingContext.ModelName,
                $"Invalid JSON for {bindingContext.ModelName}: {ex.Message}");
        }

        return Task.CompletedTask;
    }
}
