using AutoMapper;
using fix_it_tracker_back_end.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace fix_it_tracker_back_end_unit_tests.Repositories
{
    public static class UnitTestsMapping
    {
        public static IMapper GetMapper()
        {
            var _mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfiles());
            });

            return _mapperConfiguration.CreateMapper();
        }
    }
}
