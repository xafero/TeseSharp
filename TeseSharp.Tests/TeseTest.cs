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
			Assert.Equals(1, cus.Id);
			Assert.Equals("Harry", cus.FirstName);
			Assert.Equals("Johnson", cus.LastName);
			Assert.Equals(123.89, cus.Money);
			Assert.Equals(true, cus.Male);
			Assert.Equals('m', cus.Sex);
			Assert.Equals(42, cus.Houses);
			Assert.Equals(13, cus.Pets);
			Assert.Equals(97.5f, cus.Crazyness);
			Assert.Equals((byte)7, cus.Bits);
			Assert.Equals(BigInteger.One * 10, cus.Sleep);
			Assert.Equals(decimal.One, cus.Awake);
			Assert.Equals(4238249348L, cus.Birth.Ticks);
			Assert.Equals(22, cus.Home.Number);
			Assert.Equals(50023, cus.Home.Postal);
			Assert.Equals("West Ohio Street", cus.Home.Street);
			Assert.Equals(1, cus.Home.City.Code);
			Assert.Equals("Ankeny", cus.Home.City.Name);
			Assert.Equals(State.IA, cus.Home.City.State);			
		}

		private String Deflatten(string txt)
		{
			string nl = string.Format("%n");
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
			string nl = string.Format("%n");
			txt = txt.Split(new[] { nl }, 2, StringSplitOptions.None)[1].Trim();
			txt = txt.Replace(nl, " ~ ");
			return txt;
		}
	}
}