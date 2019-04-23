using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Interfaces;
using NUnit.Framework;

namespace Microwave.Integration.Test
{
    [TestFixture]
    public class IT02_DisplayOut
    {
        private Display uut;
        private IOutput output;

        [SetUp]
        public void Setup()
        {
            output = new Output();
            uut = new Display(output);
        }

        [Test]
        public void ShowTime_ZeroMinuteZeroSeconds_CorrectOutput()
        {
            uut.ShowTime(0, 0);
        }

        [Test]
        public void ShowTime_ZeroMinuteSomeSecond_CorrectOutput()
        {
            uut.ShowTime(0, 5);
        }

        [Test]
        public void ShowTime_SomeMinuteZeroSecond_CorrectOutput()
        {
            uut.ShowTime(5, 0);
        }

        [Test]
        public void ShowTime_SomeMinuteSomeSecond_CorrectOutput()
        {
            uut.ShowTime(10, 15);
        }

        [Test]
        public void ShowPower_Zero_CorrectOutput()
        {
            uut.ShowPower(0);
        }

        [Test]
        public void ShowPower_NotZero_CorrectOutput()
        {
            uut.ShowPower(150);
        }

        [Test]
        public void Clear_CorrectOutput()
        {
            uut.Clear();
        }
    }
}
