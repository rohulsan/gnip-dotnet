﻿using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace Gnip.Client
{
    [TestFixture]
    public class GnipConnectionMiscTestCase : GnipTestCase
    {
        [TestFixtureSetUp]
        public override void SetUp()
        {
            base.SetUp();
        }

        [TestFixtureTearDown]
        public override void TearDown()
        {
            base.TearDown();
        }

        [Test]
        public void TestCalculateServerDelta()
        {
            TimeSpan span = base.gnipConnection.GetServerTimeDelta();
            base.gnipConnection.TimeCorrection = span;
            Assert.AreEqual(span, base.gnipConnection.TimeCorrection);
        }
    }
}
