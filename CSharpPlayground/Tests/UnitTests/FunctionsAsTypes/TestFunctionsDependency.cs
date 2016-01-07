using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests.FunctionsAsTypes
{
    /// <summary>
    /// Not actually a Unit Test.
    /// Just compare Buy a Car with/without using closure
    /// </summary>
    [TestClass]
    public class TestFunctionsDependency
    {
        private class Car       {} // To build a running car, need selected Engine & Wheel. Also need to add some gas.
        private class CarEngine {} // Selected Engine provided by the Customer
        private class CarWheel  {} // Selected Wheel  provided by the Customer
        private class CarGas    {} // Need Gas so Car can run. Provided by car vending machine (not Customer).

        private static class CarFactory
        {
            public static Car BuildARunningCar(CarEngine engine, CarWheel wheel, CarGas gas)
            {
                return new Car();
            }
        }

        private static class BuildCarWithoutClosure
        {
            private static class CarVendingMachine
            {
                private static void MustDoCarTesting(Car car)
                {
                }

                private static CarGas GetSomeCarGas()
                {
                    return new CarGas();
                }

                public static Car GetACarFromFactory(CarEngine engine, CarWheel wheel)
                {
                    CarGas gas = GetSomeCarGas();
                    Car runningCar = CarFactory.BuildARunningCar(engine, wheel, gas); // VendingMachine has the knowledge of Engine & Wheel at this point
                    MustDoCarTesting(runningCar);
                    return runningCar;
                }
            }

            private static class Customer
            {
                public static Car BuyACar()
                {
                    CarEngine selectedEngine = new CarEngine(); //Select an Engine you like
                    CarWheel selectedWheel = new CarWheel();    //Select a  Wheel  you like

                    Car car = CarVendingMachine.GetACarFromFactory(selectedEngine, selectedWheel);
                    return car;
                }
            }
        }

        private static class BuildCarWithClosure
        {
            private static class CarVendingMachine
            {
                private static void MustDoCarTesting(Car car)
                {
                }

                private static CarGas GetSomeCarGas()
                {
                    return new CarGas();
                }

                public static Car GetACarFromFactory(Func<CarGas, Car> getARunningCarWithGas)
                {
                    CarGas gas = GetSomeCarGas();
                    Car runningCar = getARunningCarWithGas(gas);  // VendingMachine don't have the knowledge of Engine & Wheel. Only provide the Gas.
                    MustDoCarTesting(runningCar);
                    return runningCar;
                }
            }

            private static class Customer
            {
                public static Car BuyACar()
                {
                    CarEngine selectedEngine = new CarEngine(); //Select an Engine you like
                    CarWheel selectedWheel = new CarWheel();    //Select a  Wheel  you like

                    // Only Caller has the knowledge of Engine & Wheel
                    Func<CarGas, Car> getARunningCarWithGas = gas => CarFactory.BuildARunningCar(selectedEngine, selectedWheel, gas);

                    Car car = CarVendingMachine.GetACarFromFactory(getARunningCarWithGas);
                    return car;
                }
            }

        }

    }
}
