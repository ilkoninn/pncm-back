using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Exceptions.PetExceptions
{
    public class AtLeastOnePetImageException : Exception
    {
        public AtLeastOnePetImageException() : base("Ən az bir ədəd heyvan şəkili yerləşdirilməlidir.") { }
    }
}
