using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoMapperExample.Models
{
    public class AutoMapperr
    {
        public AutoMapperExample.Models.Student Mapping(Person person)
        {
            var config = new MapperConfiguration(x => { x.CreateMap<Person, Student>(); });
            IMapper mapper = config.CreateMapper();
            var dest = mapper.Map<Person,Student>(person);
            return dest;
        }
    }
}