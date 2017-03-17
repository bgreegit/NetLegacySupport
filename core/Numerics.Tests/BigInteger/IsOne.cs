// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using NUnit.Framework;

namespace System.Numerics.Tests
{
    public class IsOneTest
    {
        private static int s_seed = 0;

        [Test]
#if NET20
        public static void RunIsOneTests_NET20()
#elif NET35
        public static void RunIsOneTests_NET35()
#elif NET40
        public static void RunIsOneTests_NET40()
#endif
        {
            Random random = new Random(s_seed);

            //Just basic tests
            // Zero
            VerifyIsOne(BigInteger.Zero, false);

            // Negative One
            VerifyIsOne(BigInteger.MinusOne, false);

            // One
            VerifyIsOne(BigInteger.One, true);

            // -Int32.MaxValue
            VerifyIsOne((BigInteger)Int32.MaxValue * -1, false);

            // Int32.MaxValue
            VerifyIsOne((BigInteger)Int32.MaxValue, false);

            // int32.MaxValue + 1
            VerifyIsOne((BigInteger)Int32.MaxValue + 1, false);

            // UInt32.MaxValue
            VerifyIsOne((BigInteger)UInt32.MaxValue, false);

            // Uint32.MaxValue + 1
            VerifyIsOne((BigInteger)UInt32.MaxValue + 1, false);
        }

        private static void VerifyIsOne(BigInteger bigInt, bool expectedAnswer)
        {
            Assert.AreEqual(expectedAnswer, bigInt.IsOne);
        }
    }
}
