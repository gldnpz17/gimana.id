using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Mapper
{
    public class MapperConfig
    {
        public MapperConfiguration MapperConfiguration
        {
            get
            {
                return new MapperConfiguration((config) => 
                {

                });
            }
        }
    }
}
