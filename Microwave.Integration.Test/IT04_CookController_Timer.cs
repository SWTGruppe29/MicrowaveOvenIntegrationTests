﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute;
using NUnit.Framework;
using Timer = MicrowaveOvenClasses.Boundary.Timer;

namespace Microwave.Integration.Test
{
    [TestFixture]
    public class IT04_CookController_Timer
    {
        private ICookController _cookController;
        private IDisplay _display;
        private IPowerTube _powerTube;
        private ITimer _N;
        private IUserInterface _userInterface;

        [SetUp]
        public void Setup()
        {
            _display = Substitute.For<IDisplay>();
            _powerTube = Substitute.For<IPowerTube>();
            _userInterface = Substitute.For<IUserInterface>();
            _N = new Timer();
            _cookController = new CookController(_N,_display,_powerTube,_userInterface);
        }

        [TestCase(1000)]
        [TestCase(2000)]
        [TestCase(3000)]
        [TestCase(5000)]
        [TestCase(8000)]
        public void TimerExpired_PowerTubeOff_Correct(int time)
        {
            _cookController.StartCooking(50,time/1000);
            Thread.Sleep(time+100);
            _powerTube.Received(1).TurnOn(50);
            _powerTube.Received(1).TurnOff();
        }

        [TestCase(1000)]
        [TestCase(2000)]
        [TestCase(10000)]
        [TestCase(5000)]
        public void TimerNotExpired_PowerTubeStillOn(int time)
        {
            _cookController.StartCooking(50 , time/1000);
            Thread.Sleep(time-100);
            _powerTube.Received(0).TurnOff();
        }

        [TestCase(1000)]
        [TestCase(5000)]
        [TestCase(6000)]
        [TestCase(3000)]
        public void TimerTickEvent_OutputOnDisplay_CorrectnumberOfTimesCalled(int time)
        {
            _cookController.StartCooking(50, time/1000);
            Thread.Sleep(time+100); //Wait till done cooking
            _display.ReceivedWithAnyArgs(time/1000).ShowTime(1,1); //Check that display updated correct number of times. Timer event fired correctly
        }

        [TestCase(500,5)]
        [TestCase(200,10)]
        public void TimerExpired_UICookingIsDoneIsCalled(int power, int time)
        {
            _cookController.StartCooking(power,time);
            Thread.Sleep(time*1000 + 1000);
            _userInterface.Received(1).CookingIsDone();
        }



    }
}
