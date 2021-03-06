﻿using System.Security.Cryptography;
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
    public class IT08_UI_StartPowerTime
    {
        //Already integrated
        
        private IDoor _door;
        private ILight _light;
        private IDisplay _display;
        private IButton _startButton;
        private IButton _timeButton;
        private IButton _powerButton;
        private ITimer _timer;
        private IOutput _output;
        private IPowerTube _powerTube;

        //To integrate
        private ICookController _cookController;
        private IUserInterface _userInterface;


        [SetUp]
        public void Setup()
        {
            _door = new Door();
            _startButton = new Button();
            _timeButton = new Button();
            _powerButton = new Button();
            _powerTube = new PowerTube(_output);
            _light = Substitute.For<ILight>();
            _display = Substitute.For<IDisplay>();
            _cookController = Substitute.For<ICookController>();
            _userInterface = new UserInterface(_powerButton,_timeButton,_startButton,_door,_display,_light,_cookController);
        }

        [Test]
        public void PowerButtonPressed_DisplayUpdated()
        {
            _powerButton.Press();
            _display.ReceivedWithAnyArgs(1).ShowPower(1);
        }

        [TestCase(1)]
        [TestCase(4)]
        [TestCase(3)]
        [TestCase(7)]
        [TestCase(10)]
        public void PowerButtonPressed_PowerIncreasedAndDisplayUpdated(int times)
        {
            for (int i = 0; i < times; ++i)
            {
                _powerButton.Press();
            };
            
            _display.ReceivedWithAnyArgs(times).ShowPower(2);
        }
        [Test]
        public void StartCancelButtonPressedInPowerMode_DisplayCleared()
        {
            _powerButton.Press();
            _startButton.Press();
        }

        [TestCase(1)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(9)]
        [TestCase(6)]
        public void TimeButtonPressed_TimeIncreasedAndDisplayUpdated(int times)
        {
            _powerButton.Press();
            for (int i = 0; i < times; ++i)
            {
                _timeButton.Press();
            }
            _display.ReceivedWithAnyArgs(times).ShowTime(1,1);
        }
        [Test]
        public void StartCancelButtonPressed_StartCookingCalled()
        {
            _powerButton.Press();
            _timeButton.Press();
            _startButton.Press();
            _cookController.Received(1).StartCooking(50,1);
        }

        [Test]
        public void StartCancelButtonPressedTwice_CookingCanceled()
        {
            _powerButton.Press();
            _timeButton.Press();
            _startButton.Press();
            _startButton.Press();
            _cookController.Received(1).Stop();
        }

        [TestCase(2)]
        [TestCase(5)]
        [TestCase(4)]
        [TestCase(8)]
        [TestCase(100)]
        public void TimeButtonPressed_TimeIncreasedToCorrectAmmount(int times)
        {
            _powerButton.Press();
            for (int i = 0; i < times; ++i)
            {
                _timeButton.Press();
            }
            _display.Received(1).ShowTime(times/60,times%60);
        }

        [TestCase(5)]
        [TestCase(3)]
        [TestCase(7)]
        public void PowerButtonPressed_PowerIncreasedToCorrectAmmount(int times)
        {
            for (int i = 0; i < times; ++i)
            {
                _powerButton.Press();
            }
            _display.Received(1).ShowPower(times*50);
        }

        [Test]
        public void PowerSetToMax_PowerButtonPressed_PowertNotIncreasedToMoreThanMax()
        {
            for (int i = 0; i < 15; ++i)
            {
                _powerButton.Press();
            }
            _display.Received(2).ShowPower(50); //Goes back to 50 from 700
        }

        [TestCase(10, 5)]
        [TestCase(11, 4)]
        [TestCase(8, 5)]
        [TestCase(40, 3)]
        public void StartButtonPressed_StartCookingCalledWithCorrectParameters(int timeButton, int powerButton)
        {
            for (int i = 0; i < powerButton; ++i)
            {
                _powerButton.Press();
            }
            for (int i = 0; i < timeButton; ++i)
            {
                _timeButton.Press();
            }
            _startButton.Press();
            _cookController.Received(1).StartCooking(powerButton*50,timeButton);
        }
        [Test]
        public void StartCancelButtonPressed_ResetValuesAndClearDisplay_CorrectOutput()
        {
            _powerButton.Press();
            _startButton.Press();
            _display.Received(1).Clear();
        }



        
    }
}
