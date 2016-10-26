using System;
using System.Collections.Generic;
using System.Numerics;
using NUnit.Framework;

namespace TeseSharp.Tests
{
	[TestFixture]
	public class TeseTest
	{
		private Tese tese;

		[SetUp]
		public void TestSetup()
		{
			tese = (new TeseBuilder()).SkipNull(true).Create();
		}

		[TearDown]
		public void TestTeardown()
		{
			tese = null;
		}

		private const string txt1 = ".id=1 ~ .bits=7 ~ .lastName=Johnson ~ .sex=m ~ .birth=1970-02-19T02\\:17\\:29.348+01\\:00 ~ .money=123.89 ~ .awake=1 ~ .home.city.state=IA ~ .firstName=Harry ~ .home.city.name=Ankeny ~ .home.city.code=1 ~ .home.number=22 ~ .houses=42 ~ .crazyness=97.5 ~ .home.postal=50023 ~ .sleep=10 ~ .male=true ~ .home.street=West Ohio Street ~ .pets=13";
		
		[Test]
		public void TestDeserialize()
		{
			Customer cus = tese.Deserialize<Customer>(Deflatten(txt1));
			Assert.AreEqual(1, cus.Id);
			Assert.AreEqual("Harry", cus.FirstName);
			Assert.AreEqual("Johnson", cus.LastName);
			Assert.AreEqual(123.89, cus.Money);
			Assert.AreEqual(true, cus.Male);
			Assert.AreEqual('m', cus.Sex);
			Assert.AreEqual(42, cus.Houses);
			Assert.AreEqual(13, cus.Pets);
			Assert.AreEqual(97.5f, cus.Crazyness);
			Assert.AreEqual((byte)7, cus.Bits);
			Assert.AreEqual(BigInteger.One * 10, cus.Sleep);
			Assert.AreEqual(decimal.One, cus.Awake);			
			Assert.AreEqual(4238249348L, (cus.Birth.Ticks - 621355968000000000L) / 10000);
			Assert.AreEqual(22, cus.Home.Number);
			Assert.AreEqual(50023, cus.Home.Postal);
			Assert.AreEqual("West Ohio Street", cus.Home.Street);
			Assert.AreEqual(1, cus.Home.City.Code);
			Assert.AreEqual("Ankeny", cus.Home.City.Name);
			Assert.AreEqual(State.IA, cus.Home.City.State);			
		}

		private String Deflatten(string txt)
		{
			string nl = Environment.NewLine;
			txt = txt.Replace(" ~ ", nl);
			return txt;
		}
		
		[Test]
		public void TestSerialize()
		{
			Customer cus = new Customer(1, "Harry", "Johnson", 123.89, true, 'm', 42, (short)13, 97.5f, (byte)7,
				               BigInteger.One * 10, decimal.One, new DateTime(4238249348L),
				               new Address("West Ohio Street", 22, 50023, new City("Ankeny", State.IA, 1L)));
			string txt = Flatten(tese.Serialize(cus));
			Assert.Equals(txt1, txt);			
		}

		private String Flatten(string txt)
		{
			string nl = Environment.NewLine;
			txt = txt.Split(new[] { nl }, 2, StringSplitOptions.None)[1].Trim();
			txt = txt.Replace(nl, " ~ ");
			return txt;
		}
	}
}