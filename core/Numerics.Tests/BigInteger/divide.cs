// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using NUnit.Framework;

namespace System.Numerics.Tests
{
    public class DivideTest
    {
        private static int s_samples = 10;
        private static Random s_random = new Random(100);
        
        [Test]
#if NET20
        public static void RunDivideTwoLargeBI_NET20()
#elif NET35
        public static void RunDivideTwoLargeBI_NET35()
#elif NET40
        public static void RunDivideTwoLargeBI_NET40()
#endif
        {
            byte[] tempByteArray1 = new byte[0];
            byte[] tempByteArray2 = new byte[0];

            // Divide Method - Two Large BigIntegers
            for (int i = 0; i < s_samples; i++)
            {
                tempByteArray1 = GetRandomByteArray(s_random);
                tempByteArray2 = GetRandomByteArray(s_random);
                VerifyDivideString(Print(tempByteArray1) + Print(tempByteArray2) + "bDivide");
            }
        }

        [Test]
#if NET20
        public static void RunDivideTwoSmallBI_NET20()
#elif NET35
        public static void RunDivideTwoSmallBI_NET35()
#elif NET40
        public static void RunDivideTwoSmallBI_NET40()
#endif
        {
            byte[] tempByteArray1 = new byte[0];
            byte[] tempByteArray2 = new byte[0];

            // Divide Method - Two Small BigIntegers
            for (int i = 0; i < s_samples; i++)
            {
                tempByteArray1 = GetRandomByteArray(s_random, 2);
                tempByteArray2 = GetRandomByteArray(s_random, 2);
                VerifyDivideString(Print(tempByteArray1) + Print(tempByteArray2) + "bDivide");
            }
        }

        [Test]
#if NET20
        public static void RunDivideOneLargeOneSmallBI_NET20()
#elif NET35
        public static void RunDivideOneLargeOneSmallBI_NET35()
#elif NET40
        public static void RunDivideOneLargeOneSmallBI_NET40()
#endif
        {
            byte[] tempByteArray1 = new byte[0];
            byte[] tempByteArray2 = new byte[0];

            // Divide Method - One large and one small BigIntegers
            for (int i = 0; i < s_samples; i++)
            {
                tempByteArray1 = GetRandomByteArray(s_random);
                tempByteArray2 = GetRandomByteArray(s_random, 2);
                VerifyDivideString(Print(tempByteArray1) + Print(tempByteArray2) + "bDivide");

                tempByteArray1 = GetRandomByteArray(s_random, 2);
                tempByteArray2 = GetRandomByteArray(s_random);
                VerifyDivideString(Print(tempByteArray1) + Print(tempByteArray2) + "bDivide");
            }
        }

        [Test]
#if NET20
        public static void RunDivideOneLargeOneZeroBI_NET20()
#elif NET35
        public static void RunDivideOneLargeOneZeroBI_NET35()
#elif NET40
        public static void RunDivideOneLargeOneZeroBI_NET40()
#endif
        {
            byte[] tempByteArray1 = new byte[0];
            byte[] tempByteArray2 = new byte[0];

            // Divide Method - One large BigIntegers and zero
            for (int i = 0; i < s_samples; i++)
            {
                tempByteArray1 = GetRandomByteArray(s_random);
                tempByteArray2 = new byte[] { 0 };
                VerifyDivideString(Print(tempByteArray1) + Print(tempByteArray2) + "bDivide");
                
                Assert.Throws<DivideByZeroException>(() =>
                {
                    VerifyDivideString(Print(tempByteArray2) + Print(tempByteArray1) + "bDivide");
                });
            }
        }

        [Test]
#if NET20
        public static void RunDivideOneSmallOneZeroBI_NET20()
#elif NET35
        public static void RunDivideOneSmallOneZeroBI_NET35()
#elif NET40
        public static void RunDivideOneSmallOneZeroBI_NET40()
#endif
        {
            byte[] tempByteArray1 = new byte[0];
            byte[] tempByteArray2 = new byte[0];

            // Divide Method - One small BigIntegers and zero
            for (int i = 0; i < s_samples; i++)
            {
                tempByteArray1 = GetRandomByteArray(s_random, 2);
                tempByteArray2 = new byte[] { 0 };
                VerifyDivideString(Print(tempByteArray1) + Print(tempByteArray2) + "bDivide");

                Assert.Throws<DivideByZeroException>(() =>
                {
                    VerifyDivideString(Print(tempByteArray2) + Print(tempByteArray1) + "bDivide");
                });
            }
        }

        [Test]
#if NET20
        public static void RunDivideOneOverOne_NET20()
#elif NET35
        public static void RunDivideOneOverOne_NET35()
#elif NET40
        public static void RunDivideOneOverOne_NET40()
#endif
        {
            byte[] tempByteArray1 = new byte[0];
            byte[] tempByteArray2 = new byte[0];

            // Axiom: X/1 = X
            VerifyIdentityString(BigInteger.One + " " + Int32.MaxValue + " bDivide", Int32.MaxValue.ToString());
            VerifyIdentityString(BigInteger.One + " " + Int64.MaxValue + " bDivide", Int64.MaxValue.ToString());

            for (int i = 0; i < s_samples; i++)
            {
                String randBigInt = Print(GetRandomByteArray(s_random));
                VerifyIdentityString(BigInteger.One + " " + randBigInt + "bDivide", randBigInt.Substring(0, randBigInt.Length - 1));
            }
        }


        [Test]
#if NET20
        public static void RunDivideZeroOverBI_NET20()
#elif NET35
        public static void RunDivideZeroOverBI_NET35()
#elif NET40
        public static void RunDivideZeroOverBI_NET40()
#endif
        {
            byte[] tempByteArray1 = new byte[0];
            byte[] tempByteArray2 = new byte[0];

            // Axiom: 0/X = 0
            VerifyIdentityString(Int32.MaxValue + " " + BigInteger.Zero + " bDivide", BigInteger.Zero.ToString());
            VerifyIdentityString(Int64.MaxValue + " " + BigInteger.Zero + " bDivide", BigInteger.Zero.ToString());

            for (int i = 0; i < s_samples; i++)
            {
                String randBigInt = Print(GetRandomByteArray(s_random));
                VerifyIdentityString(randBigInt + BigInteger.Zero + " bDivide", BigInteger.Zero.ToString());
            }
        }

        [Test]
#if NET20
        public static void RunDivideBoundary_NET20()
#elif NET35
        public static void RunDivideBoundary_NET35()
#elif NET40
        public static void RunDivideBoundary_NET40()
#endif
        {
            byte[] tempByteArray1 = new byte[0];
            byte[] tempByteArray2 = new byte[0];

            // Check interesting cases for boundary conditions
            // You'll either be shifting a 0 or 1 across the boundary
            // 32 bit boundary  n2=0
            VerifyDivideString(Math.Pow(2, 32) + " 2 bDivide");

            // 32 bit boundary  n1=0 n2=1
            VerifyDivideString(Math.Pow(2, 33) + " 2 bDivide");
        }

        [Test]
#if NET20
        public static void RunOverflow_NET20()
#elif NET35
        public static void RunOverflow_NET35()
#elif NET40
        public static void RunOverflow_NET40()
#endif
        {
            // these values lead to an "overflow", if dividing digit by digit
            // we need to ensure that this case is being handled accordingly...
            var x = new BigInteger(new byte[] { 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xFE, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0 });
            var y = new BigInteger(new byte[] { 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0 });
            var z = new BigInteger(new byte[] { 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0 });

            Assert.AreEqual(z, BigInteger.Divide(x, y));
        }

        private static void VerifyDivideString(string opstring)
        {
            StackCalc sc = new StackCalc(opstring);
            while (sc.DoNextOperation())
            {
                Assert.AreEqual(sc.snCalc.Peek().ToString(), sc.myCalc.Peek().ToString());
            }
        }

        private static void VerifyIdentityString(string opstring1, string opstring2)
        {
            StackCalc sc1 = new StackCalc(opstring1);
            while (sc1.DoNextOperation())
            {	
                //Run the full calculation
                sc1.DoNextOperation();
            }

            StackCalc sc2 = new StackCalc(opstring2);
            while (sc2.DoNextOperation())
            {	
                //Run the full calculation
                sc2.DoNextOperation();
            }

            Assert.AreEqual(sc1.snCalc.Peek().ToString(), sc2.snCalc.Peek().ToString());
        }

        private static byte[] GetRandomByteArray(Random random)
        {
            return GetRandomByteArray(random, random.Next(1, 100));
        }

        private static byte[] GetRandomByteArray(Random random, int size)
        {
            return MyBigIntImp.GetNonZeroRandomByteArray(random, size);
        }
        
        private static String Print(byte[] bytes)
        {
            return MyBigIntImp.Print(bytes);
        }
    }
}
