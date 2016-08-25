using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpPlayground
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("Hello");

            SomeUtil.SimpleData[] simpleDatasSame = new SomeUtil.SimpleData[]
            {
                new SomeUtil.SimpleData(1, new int[] { 1,2}, new decimal[] { 10m, 20m}),
                new SomeUtil.SimpleData(1, new int[] { 1,2}, new decimal[] { 10m, 20m}),
                new SomeUtil.SimpleData(1, new int[] { 1,2}, new decimal[] { 10m, 20m})
            };

            SomeUtil.SimpleData[] simpleDatasDifferent = new SomeUtil.SimpleData[]
            {
                new SomeUtil.SimpleData(1, new int[] { 1,2}, new decimal[] { 10m, 20m}),
                new SomeUtil.SimpleData(1, new int[] { 1,2,3}, new decimal[] { 10m, 20m}),
                new SomeUtil.SimpleData(1, new int[] { 1,2,3,4}, new decimal[] { 10m, 20m})
            };

            //var a = simpleDatasSame.Distinct(new SomeUtil.SimpleDataComparer()).ToArray();
            var b = simpleDatasDifferent.Distinct(new SomeUtil.SimpleDataComparer()).ToArray();
        }
    }

    public static class SomeUtil
    {
        public sealed class SimpleData
        {
            public SimpleData(int a, int[] b, decimal[] c)
            {
                AInt = a;
                SomeInts = b;
                SomeDecimals = c;
            }

            public readonly int AInt;
            public readonly int[] SomeInts;            
            public readonly decimal[] SomeDecimals;
        }

        public class SimpleDataComparer : IEqualityComparer<SimpleData>
        {
            public bool Equals(SimpleData d1, SimpleData d2)
            {
                System.Diagnostics.Debug.WriteLine("d1: " + d1.AInt + " [" + d1.SomeInts.Length +"]" + " | d2: " + d2.AInt + " [" + d2.SomeInts.Length + "]");

                return d1.AInt.Equals(d2.AInt)
                    && d1.SomeInts.SequenceEqual(d2.SomeInts)
                    && d1.SomeDecimals.SequenceEqual(d2.SomeDecimals);
            }

            public int GetHashCode(SimpleData obj)
            {
                System.Diagnostics.Debug.WriteLine("Hash: " + obj.AInt + "[" +obj.SomeInts.Length+"]");

                var a = obj.AInt.GetHashCode();
                var b = obj.SomeInts.GetHashCode();
                var c = obj.SomeDecimals.GetHashCode();

                var code = a ^ b ^ c;


                //var code = obj.MaxPaylines ^ obj.ValidPaylineIndices ^ obj.WagerProgression;

                return a;
            }
        }
    }


}
    