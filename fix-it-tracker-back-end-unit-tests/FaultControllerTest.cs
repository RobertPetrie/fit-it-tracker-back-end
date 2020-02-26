﻿using fix_it_tracker_back_end.Controllers;
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
    public class FaultControllerTest
    {
        private FaultController _faultController;
        private IFixItTrackerRepository _fixItTrackerRepository;

        private static int EXISTING_FAULT_ID = 1;
        private static int NON_EXISTING_FAULT_ID = 25;
        private static int NUM_OF_FAULTS = 7;

        public FaultControllerTest()
        {
            _fixItTrackerRepository = new UnitTestsRepository();
            _faultController = new FaultController(_fixItTrackerRepository, UnitTestsMapping.GetMapper());
        }

        // GET api/fault
        [Fact]
        public void GetFaults_ReturnsOkResult()
        {
            var okResult = _faultController.GetFaults();
            Assert.IsType<OkObjectResult>(okResult.Result);
        }

        [Fact]
        public void GetFaults_ReturnsRightItem()
        {
            var okResult = _faultController.GetFaults().Result as OkObjectResult;
            Assert.Equal(EXISTING_FAULT_ID, (okResult.Value as List<FaultGetDto>).FirstOrDefault(f => f.FaultID == EXISTING_FAULT_ID).FaultID);
        }

        [Fact]
        public void GetFaults_ReturnsAllItems()
        {
            var okResult = _faultController.GetFaults().Result as OkObjectResult;
            var items = Assert.IsType<List<FaultGetDto>>(okResult.Value);
            Assert.Equal(NUM_OF_FAULTS, items.Count);
        }

        [Fact]
        public void GetFaults_ReturnsNotFound()
        {
            _fixItTrackerRepository = new UnitTestsRepository(noFaults: true);
            _faultController = new FaultController(_fixItTrackerRepository, UnitTestsMapping.GetMapper());

            var okResult = _faultController.GetFaults();
            Assert.IsType<NotFoundObjectResult>(okResult.Result);
        }

        // GET api/fault/5
        [Fact]
        public void GetFault_ReturnsOkResult()
        {
            var okResult = _faultController.GetFault(EXISTING_FAULT_ID);
            Assert.IsType<OkObjectResult>(okResult.Result);
        }

        [Fact]
        public void GetFault_ReturnsRightItem()
        {
            var okResult = _faultController.GetFault(EXISTING_FAULT_ID).Result as OkObjectResult;
            Assert.IsType<FaultGetDto>(okResult.Value);
            Assert.Equal(EXISTING_FAULT_ID, (okResult.Value as FaultGetDto).FaultID);
        }

        [Fact]
        public void GetFault_ReturnsNotFound()
        {
            var okResult = _faultController.GetFault(NON_EXISTING_FAULT_ID);
            Assert.IsType<NotFoundObjectResult>(okResult.Result);
        }
    }
}
