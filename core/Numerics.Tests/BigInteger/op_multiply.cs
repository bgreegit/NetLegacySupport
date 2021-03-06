// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using NUnit.Framework;

namespace System.Numerics.Tests
{
    public class op_multiplyTest
    {
        private static int s_samples = 10;
        private static Random s_random = new Random(100);

        [Test]
#if NET20
        public static void RunMultiplyPositive_NET20()
#elif NET35
        public static void RunMultiplyPositive_NET35()
#elif NET40
        public static void RunMultiplyPositive_NET40()
#endif
        {
            byte[] tempByteArray1 = new byte[0];
            byte[] tempByteArray2 = new byte[0];

            // Multiply Method - One Large BigInteger
            for (int i = 0; i < s_samples; i++)
            {
                tempByteArray1 = GetRandomByteArray(s_random);
                VerifyMultiplyString(Print(tempByteArray1) + "u*");
            }

            // Multiply Method - Two Large BigIntegers
            for (int i = 0; i < s_samples; i++)
            {
                tempByteArray1 = GetRandomByteArray(s_random);
                tempByteArray2 = GetRandomByteArray(s_random);
                VerifyMultiplyString(Print(tempByteArray1) + Print(tempByteArray2) + "b*");
            }

            // Multiply Method - Two Small BigIntegers
            for (int i = 0; i < s_samples; i++)
            {
                tempByteArray1 = GetRandomByteArray(s_random, 2);
                tempByteArray2 = GetRandomByteArray(s_random, 2);
                VerifyMultiplyString(Print(tempByteArray1) + Print(tempByteArray2) + "b*");
            }

            // Multiply Method - One large and one small BigIntegers
            for (int i = 0; i < s_samples; i++)
            {
                try
                {
                    tempByteArray1 = GetRandomByteArray(s_random);
                    tempByteArray2 = GetRandomByteArray(s_random, 2);
                    VerifyMultiplyString(Print(tempByteArray1) + Print(tempByteArray2) + "b*");

                    tempByteArray1 = GetRandomByteArray(s_random, 2);
                    tempByteArray2 = GetRandomByteArray(s_random);
                    VerifyMultiplyString(Print(tempByteArray1) + Print(tempByteArray2) + "b*");
                }
                catch (IndexOutOfRangeException)
                {
                    // TODO: Refactor this
                    Console.WriteLine("Array1: " + Print(tempByteArray1));
                    Console.WriteLine("Array2: " + Print(tempByteArray2));
                    throw;
                }
            }
        }

        [Test]
#if NET20
        public static void RunMultiplyPositiveWith0_NET20()
#elif NET35
        public static void RunMultiplyPositiveWith0_NET35()
#elif NET40
        public static void RunMultiplyPositiveWith0_NET40()
#endif
        {
            byte[] tempByteArray1 = new byte[0];
            byte[] tempByteArray2 = new byte[0];
            
            // Multiply Method - One large BigIntegers and zero
            for (int i = 0; i < s_samples; i++)
            {
                tempByteArray1 = GetRandomByteArray(s_random);
                tempByteArray2 = new byte[] { 0 };
                VerifyMultiplyString(Print(tempByteArray1) + Print(tempByteArray2) + "b*");

                tempByteArray1 = new byte[] { 0 };
                tempByteArray2 = GetRandomByteArray(s_random);
                VerifyMultiplyString(Print(tempByteArray1) + Print(tempByteArray2) + "b*");
            }

            // Multiply Method - One small BigIntegers and zero
            for (int i = 0; i < s_samples; i++)
            {
                tempByteArray1 = GetRandomByteArray(s_random, 2);
                tempByteArray2 = new byte[] { 0 };
                VerifyMultiplyString(Print(tempByteArray1) + Print(tempByteArray2) + "b*");

                tempByteArray1 = new byte[] { 0 };
                tempByteArray2 = GetRandomByteArray(s_random, 2);
                VerifyMultiplyString(Print(tempByteArray1) + Print(tempByteArray2) + "b*");
            }
        }

        [Test]
#if NET20
        public static void RunMultiplyAxiomXmult1_NET20()
#elif NET35
        public static void RunMultiplyAxiomXmult1_NET35()
#elif NET40
        public static void RunMultiplyAxiomXmult1_NET40()
#endif
        {
            byte[] tempByteArray1 = new byte[0];
            byte[] tempByteArray2 = new byte[0];

            // Axiom: X*1 = X
            VerifyIdentityString(Int32.MaxValue + " " + BigInteger.One + " b*", Int32.MaxValue.ToString());
            VerifyIdentityString(Int64.MaxValue + " " + BigInteger.One + " b*", Int64.MaxValue.ToString());

            for (int i = 0; i < s_samples; i++)
            {
                String randBigInt = Print(GetRandomByteArray(s_random));
                VerifyIdentityString(randBigInt + BigInteger.One + " b*", randBigInt + "u+");
            }
        }

        [Test]
#if NET20
        public static void RunMultiplyAxiomXmult0_NET20()
#elif NET35
        public static void RunMultiplyAxiomXmult0_NET35()
#elif NET40
        public static void RunMultiplyAxiomXmult0_NET40()
#endif
        {
            byte[] tempByteArray1 = new byte[0];
            byte[] tempByteArray2 = new byte[0];


            // Axiom: X*0 = 0
            VerifyIdentityString(Int32.MaxValue + " " + BigInteger.Zero + " b*", BigInteger.Zero.ToString());
            VerifyIdentityString(Int64.MaxValue + " " + BigInteger.Zero + " b*", BigInteger.Zero.ToString());

            for (int i = 0; i < s_samples; i++)
            {
                String randBigInt = Print(GetRandomByteArray(s_random));
                VerifyIdentityString(randBigInt + BigInteger.Zero + " b*", BigInteger.Zero.ToString());
            }
        }

        [Test]
#if NET20
        public static void RunMultiplyAxiomComm_NET20()
#elif NET35
        public static void RunMultiplyAxiomComm_NET35()
#elif NET40
        public static void RunMultiplyAxiomComm_NET40()
#endif
        {
            byte[] tempByteArray1 = new byte[0];
            byte[] tempByteArray2 = new byte[0];

            // Check interesting cases for boundary conditions
            // You'll either be shifting a 0 or 1 across the boundary
            // 32 bit boundary  n2=0
            VerifyMultiplyString(Math.Pow(2, 32) + " 2 b*");

            // 32 bit boundary  n1=0 n2=1
            VerifyMultiplyString(Math.Pow(2, 33) + " 2 b*");
        }

        [Test]
#if NET20
        public static void RunMultiplyBoundary_NET20()
#elif NET35
        public static void RunMultiplyBoundary_NET35()
#elif NET40
        public static void RunMultiplyBoundary_NET40()
#endif
        {
            byte[] tempByteArray1 = new byte[0];
            byte[] tempByteArray2 = new byte[0];

            // Check interesting cases for boundary conditions
            // You'll either be shifting a 0 or 1 across the boundary
            // 32 bit boundary  n2=0
            VerifyMultiplyString(Math.Pow(2, 32) + " 2 b*");

            // 32 bit boundary  n1=0 n2=1
            VerifyMultiplyString(Math.Pow(2, 33) + " 2 b*");
        }

        [Test]
#if NET20
        public static void RunMultiplyTests_NET20()
#elif NET35
        public static void RunMultiplyTests_NET35()
#elif NET40
        public static void RunMultiplyTests_NET40()
#endif
        {
            byte[] tempByteArray1 = new byte[0];
            byte[] tempByteArray2 = new byte[0];

            // Multiply Method - One Large BigInteger
            for (int i = 0; i < s_samples; i++)
            {
                tempByteArray1 = GetRandomByteArray(s_random);
                VerifyMultiplyString(Print(tempByteArray1) + "u*");
            }

            // Multiply Method - Two Large BigIntegers
            for (int i = 0; i < s_samples; i++)
            {
                tempByteArray1 = GetRandomByteArray(s_random);
                tempByteArray2 = GetRandomByteArray(s_random);
                VerifyMultiplyString(Print(tempByteArray1) + Print(tempByteArray2) + "b*");
            }

            // Multiply Method - Two Small BigIntegers
            for (int i = 0; i < s_samples; i++)
            {
                tempByteArray1 = GetRandomByteArray(s_random, 2);
                tempByteArray2 = GetRandomByteArray(s_random, 2);
                VerifyMultiplyString(Print(tempByteArray1) + Print(tempByteArray2) + "b*");
            }

            // Multiply Method - One large and one small BigIntegers
            for (int i = 0; i < s_samples; i++)
            {
                try
                {
                    tempByteArray1 = GetRandomByteArray(s_random);
                    tempByteArray2 = GetRandomByteArray(s_random, 2);
                    VerifyMultiplyString(Print(tempByteArray1) + Print(tempByteArray2) + "b*");

                    tempByteArray1 = GetRandomByteArray(s_random, 2);
                    tempByteArray2 = GetRandomByteArray(s_random);
                    VerifyMultiplyString(Print(tempByteArray1) + Print(tempByteArray2) + "b*");
                }
                catch (IndexOutOfRangeException)
                {
                    // TODO: Refactor this
                    Console.WriteLine("Array1: " + Print(tempByteArray1));
                    Console.WriteLine("Array2: " + Print(tempByteArray2));
                    throw;
                }
            }

            // Multiply Method - One large BigIntegers and zero
            for (int i = 0; i < s_samples; i++)
            {
                tempByteArray1 = GetRandomByteArray(s_random);
                tempByteArray2 = new byte[] { 0 };
                VerifyMultiplyString(Print(tempByteArray1) + Print(tempByteArray2) + "b*");

                tempByteArray1 = new byte[] { 0 };
                tempByteArray2 = GetRandomByteArray(s_random);
                VerifyMultiplyString(Print(tempByteArray1) + Print(tempByteArray2) + "b*");
            }

            // Multiply Method - One small BigIntegers and zero
            for (int i = 0; i < s_samples; i++)
            {
                tempByteArray1 = GetRandomByteArray(s_random, 2);
                tempByteArray2 = new byte[] { 0 };
                VerifyMultiplyString(Print(tempByteArray1) + Print(tempByteArray2) + "b*");

                tempByteArray1 = new byte[] { 0 };
                tempByteArray2 = GetRandomByteArray(s_random, 2);
                VerifyMultiplyString(Print(tempByteArray1) + Print(tempByteArray2) + "b*");
            }

            // Axiom: X*1 = X
            VerifyIdentityString(Int32.MaxValue + " " + BigInteger.One + " b*", Int32.MaxValue.ToString());
            VerifyIdentityString(Int64.MaxValue + " " + BigInteger.One + " b*", Int64.MaxValue.ToString());

            for (int i = 0; i < s_samples; i++)
            {
                String randBigInt = Print(GetRandomByteArray(s_random));
                VerifyIdentityString(randBigInt + BigInteger.One + " b*", randBigInt + "u+");
            }

            // Axiom: X*0 = 0
            VerifyIdentityString(Int32.MaxValue + " " + BigInteger.Zero + " b*", BigInteger.Zero.ToString());
            VerifyIdentityString(Int64.MaxValue + " " + BigInteger.Zero + " b*", BigInteger.Zero.ToString());

            for (int i = 0; i < s_samples; i++)
            {
                String randBigInt = Print(GetRandomByteArray(s_random));
                VerifyIdentityString(randBigInt + BigInteger.Zero + " b*", BigInteger.Zero.ToString());
            }

            // Axiom: a*b = b*a
            VerifyIdentityString(Int32.MaxValue + " " + Int64.MaxValue + " b*", Int64.MaxValue + " " + Int32.MaxValue + " b*");

            for (int i = 0; i < s_samples; i++)
            {
                String randBigInt1 = Print(GetRandomByteArray(s_random));
                String randBigInt2 = Print(GetRandomByteArray(s_random));
                VerifyIdentityString(randBigInt1 + randBigInt2 + "b*", randBigInt2 + randBigInt1 + "b*");
            }
            
            // Check interesting cases for boundary conditions
            // You'll either be shifting a 0 or 1 across the boundary
            // 32 bit boundary  n2=0
            VerifyMultiplyString(Math.Pow(2, 32) + " 2 b*");

            // 32 bit boundary  n1=0 n2=1
            VerifyMultiplyString(Math.Pow(2, 33) + " 2 b*");
        }

        private static void VerifyMultiplyString(string opstring)
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
            return GetRandomByteArray(random, random.Next(0, 100));
        }

        private static byte[] GetRandomByteArray(Random random, int size)
        {
            return MyBigIntImp.GetRandomByteArray(random, size);
        }

        private static String Print(byte[] bytes)
        {
            return MyBigIntImp.Print(bytes);
        }
    }
}
