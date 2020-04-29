using System;
using System.Collections.Generic;
using System.Text;
using Wikivox.Models;

namespace Wikivox.Service.Services
{
    internal class Comparer : IEqualityComparer<Album>
    {
        public bool Equals(Album x, Album y)
        {
            if (string.Equals(x.Name, y.Name, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            return false;
        }

        //public bool Equals(Album x, Album y)
        //{
        //    throw new NotImplementedException();
        //}

        public int GetHashCode(Album obj)
        {
            return obj.Name.GetHashCode();
        }

        //public int GetHashCode(Album obj)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
