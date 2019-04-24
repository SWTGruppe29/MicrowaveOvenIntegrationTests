using System;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Microwave.Integration.Test
{
    [TestFixture]
    public class IT10_UI_Display
    {
        private IButton powerButton;
        private IButton timeButton;
        private IButton startCancelButton;
        private IDoor door;
        private IDisplay display;
        private ILight light;
        private ICookController cooker;
        private IOutput output;
        private IUserInterface userInterface;

        [SetUp]
        public void Setup()
        {
            powerButton = new Button();
            timeButton = new Button();
            startCancelButton = new Button();
            door = new Door();
            output = new Output();
            display = new Display(output);
            light = new Light(output);
            cooker = Substitute.For<ICookController>();
            
            userInterface = new UserInterface(
                powerButton, 
                timeButton, 
                startCancelButton, 
                door,
                display, 
                light, 
                cooker);
        }

        [Test]
        public void OnPowerPressed_StateReady() //Tests both state ready and setPower
        {
            for (int i = 0 ; i < 15; i ++)
            userInterface.OnPowerPressed(new object(), EventArgs.Empty);

            //Displays 50W -> 100W -> ...... -> 700W -> 50W
        }

        [Test]
        public void OnTimePressed_StateSetPower()
        {
            userInterface.OnPowerPressed(new object(), EventArgs.Empty); //Set state to SetPower

            for (int i = 0; i < 20; i++)
            userInterface.OnTimePressed(new object(), EventArgs.Empty);
        }
        
        [Test]
        public void OnStartCancelPressed_StateSetPower()
        {
            userInterface.OnPowerPressed(new object(), EventArgs.Empty); //Set state to SetPower

            userInterface.OnStartCancelPressed(new object(), EventArgs.Empty); //Startbtn
        }

        [Test]
        public void OnStartCancelPressed_StateSetTime()
        {
            userInterface.OnPowerPressed(new object(), EventArgs.Empty); //Set state to SetPower
            userInterface.OnTimePressed(new object(), EventArgs.Empty); //Set state to SetTime

            userInterface.OnStartCancelPressed(new object(), EventArgs.Empty); //Startbtn
        }

        [Test]
        public void OnStartCancelPressed_StateCooking()
        {
            userInterface.OnPowerPressed(new object(), EventArgs.Empty); //Set state to SetPower
            userInterface.OnTimePressed(new object(), EventArgs.Empty); //Set state to SetTime

            userInterface.OnStartCancelPressed(new object(), EventArgs.Empty); //SetState Cooking
            userInterface.OnStartCancelPressed(new object(), EventArgs.Empty); //Startbtn to cancel
        }

        [Test]
        public void CookingIsDone_StateCooking()
        {
            userInterface.OnPowerPressed(new object(), EventArgs.Empty); //Set state to SetPower
            userInterface.OnTimePressed(new object(), EventArgs.Empty); //Set state to SetTime

            userInterface.OnStartCancelPressed(new object(), EventArgs.Empty); //SetState Cooking
            userInterface.CookingIsDone();
        }

        [Test]
        public void OnDoorOpened_StateSetTime()
        {
            userInterface.OnPowerPressed(new object(), EventArgs.Empty); //Set state to SetPower
            userInterface.OnTimePressed(new object(), EventArgs.Empty); //Set state to SetTime

            userInterface.OnDoorOpened(new object(), EventArgs.Empty);
        }

        [Test]
        public void OnDoorOpened_StateSetPower()
        {
            userInterface.OnPowerPressed(new object(), EventArgs.Empty); //Set state to SetPower
            
            userInterface.OnDoorOpened(new object(), EventArgs.Empty);
        }
    }
}
