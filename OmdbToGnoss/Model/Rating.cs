using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmdbToGnoss.Model
{
    public class Rating
    {
        private string _value;

        public string Source { get; set; }
        public string Value {
            get
            {
                if (Source.Equals("Internet Movie Database"))
                {
                    if (_value.Contains("/"))
                    {
                        _value = _value.Replace(_value.Substring(_value.IndexOf("/")), "").Replace(".", "");
                    }
                }
                else if (Source.Equals("Rotten Tomatoes"))
                {
                    if (_value.Contains("%"))
                    {
                        _value = _value.Replace("%", "");
                    }
                }
                else if (Source.Equals("Metacritic"))
                {
                    if (_value.Contains("/"))
                    {
                        _value = _value.Replace(_value.Substring(_value.IndexOf("/")), "").Replace(".", "");
                    }
                }

                return _value;
            }
            set
            {
                _value = value;
            }
        }
    }
}
