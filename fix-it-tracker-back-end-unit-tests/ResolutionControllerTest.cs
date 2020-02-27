using fix_it_tracker_back_end.Controllers;
using fix_it_tracker_back_end.Data.Repositories;
using fix_it_tracker_back_end.Dtos;
using fix_it_tracker_back_end_unit_tests.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace fix_it_tracker_back_end_unit_tests
{
    public class ResolutionControllerTest
    {
        private ResolutionController _resolutionController;
        private IFixItTrackerRepository _fixItTrackerRepository;

        private static int EXISTING_RESOLUTION_ID = 1;
        private static int NON_EXISTING_RESOLUTION_ID = 25;
        private static int NUM_OF_RESOLUTIONS = 5;

        public ResolutionControllerTest()
        {
            _fixItTrackerRepository = new UnitTestsRepository();
            _resolutionController = new ResolutionController(_fixItTrackerRepository, UnitTestsMapping.GetMapper());
        }

        // GET api/resolution
        [Fact]
        public void GetResolutions_ReturnsOkResult()
        {
            var okResult = _resolutionController.GetResolutions();
            Assert.IsType<OkObjectResult>(okResult.Result);
        }

        [Fact]
        public void GetResolutions_ReturnsRightItem()
        {
            var okResult = _resolutionController.GetResolutions().Result as OkObjectResult;
            Assert.Equal(EXISTING_RESOLUTION_ID, (okResult.Value as List<ResolutionGetDto>).FirstOrDefault(r => r.ResolutionID == EXISTING_RESOLUTION_ID).ResolutionID);
        }

        [Fact]
        public void GetResolutions_ReturnsAllItems()
        {
            var okResult = _resolutionController.GetResolutions().Result as OkObjectResult;
            var items = Assert.IsType<List<ResolutionGetDto>>(okResult.Value);
            Assert.Equal(NUM_OF_RESOLUTIONS, items.Count);
        }

        [Fact]
        public void GetResolutions_ReturnsNotFound()
        {
            _fixItTrackerRepository = new UnitTestsRepository(noResolutions: true);
            _resolutionController = new ResolutionController(_fixItTrackerRepository, UnitTestsMapping.GetMapper());

            var okResult = _resolutionController.GetResolutions();
            Assert.IsType<NotFoundObjectResult>(okResult.Result);
        }

        // GET api/resolution/5
        [Fact]
        public void GetResolution_ReturnsOkResult()
        {
            var okResult = _resolutionController.GetResolution(EXISTING_RESOLUTION_ID);
            Assert.IsType<OkObjectResult>(okResult.Result);
        }

        [Fact]
        public void GetResolution_ReturnsRightItem()
        {
            var okResult = _resolutionController.GetResolution(EXISTING_RESOLUTION_ID).Result as OkObjectResult;
            Assert.IsType<ResolutionGetDto>(okResult.Value);
            Assert.Equal(EXISTING_RESOLUTION_ID, (okResult.Value as ResolutionGetDto).ResolutionID);
        }

        [Fact]
        public void GetResolution_ReturnsNotFound()
        {
            var okResult = _resolutionController.GetResolution(NON_EXISTING_RESOLUTION_ID);
            Assert.IsType<NotFoundObjectResult>(okResult.Result);
        }
    }
}
