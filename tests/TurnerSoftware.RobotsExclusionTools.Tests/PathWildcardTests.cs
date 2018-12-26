﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TurnerSoftware.RobotsExclusionTools.Tests
{
	[TestClass]
	public class PathWildcardTests : TestBase
	{
		[TestMethod]
		public void ExplicitWildcardSuffix()
		{
			var robotsFile = GetRobotsFile("Comprehensive-Example.txt");
			var userAgent = "ExplicitWildcardSuffix";

			Assert.IsFalse(robotsFile.IsAllowedAccess(new Uri("http://www.example.org/"), userAgent));
			Assert.IsFalse(robotsFile.IsAllowedAccess(new Uri("http://www.example.org/org/about.html"), userAgent));
			Assert.IsTrue(robotsFile.IsAllowedAccess(new Uri("http://www.example.org/org/plan.html"), userAgent));
			Assert.IsTrue(robotsFile.IsAllowedAccess(new Uri("http://www.example.org/organization/plan.html"), userAgent));
		}

		[TestMethod]
		public void ExplicitWildcardPrefix()
		{
			var robotsFile = GetRobotsFile("Comprehensive-Example.txt");
			var userAgent = "ExplicitWildcardPrefix";

			Assert.IsFalse(robotsFile.IsAllowedAccess(new Uri("http://www.example.org/"), userAgent));
			Assert.IsTrue(robotsFile.IsAllowedAccess(new Uri("http://www.example.org/org/about.html"), userAgent));
			Assert.IsTrue(robotsFile.IsAllowedAccess(new Uri("http://www.example.org/org/plan.html"), userAgent));
			Assert.IsTrue(robotsFile.IsAllowedAccess(new Uri("http://www.example.org/organization/plan.html"), userAgent));
			Assert.IsFalse(robotsFile.IsAllowedAccess(new Uri("http://www.example.org/organization/plan.jpg"), userAgent));
			Assert.IsTrue(robotsFile.IsAllowedAccess(new Uri("http://www.example.org/plan.html"), userAgent));
		}

		[TestMethod]
		public void PathWithAnyQueryString()
		{
			var robotsFile = GetRobotsFile("Comprehensive-Example.txt");
			var userAgent = "PathWithAnyQueryString";

			Assert.IsTrue(robotsFile.IsAllowedAccess(new Uri("http://www.example.org/"), userAgent));
			Assert.IsTrue(robotsFile.IsAllowedAccess(new Uri("http://www.example.org/org/about.html"), userAgent));
			Assert.IsFalse(robotsFile.IsAllowedAccess(new Uri("http://www.example.org/org/plan.html?"), userAgent));
			Assert.IsFalse(robotsFile.IsAllowedAccess(new Uri("http://www.example.org/org/plan.html?foo=bar"), userAgent));
		}

		[TestMethod]
		public void PathWithPartQueryString()
		{
			var robotsFile = GetRobotsFile("Comprehensive-Example.txt");
			var userAgent = "PathWithPartQueryString";

			Assert.IsTrue(robotsFile.IsAllowedAccess(new Uri("http://www.example.org/"), userAgent));
			Assert.IsTrue(robotsFile.IsAllowedAccess(new Uri("http://www.example.org/org/about.html"), userAgent));
			Assert.IsTrue(robotsFile.IsAllowedAccess(new Uri("http://www.example.org/org/plan.html?"), userAgent));
			Assert.IsTrue(robotsFile.IsAllowedAccess(new Uri("http://www.example.org/org/plan.html?foo="), userAgent));
			Assert.IsFalse(robotsFile.IsAllowedAccess(new Uri("http://www.example.org/org/plan.html?foo=bar"), userAgent));
			Assert.IsFalse(robotsFile.IsAllowedAccess(new Uri("http://www.example.org/org/plan.html?foo=bar&abc=123"), userAgent));
		}
	}
}
