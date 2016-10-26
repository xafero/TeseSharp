using System;

namespace TeseSharp.Tests
{
	public class Address
	{
		private string street;

		private int number;

		private long postal;

		private City city;

		public Address()
		{
		}

		public Address(string street, int number, long postal, City city)
		{
			this.street = street;
			this.number = number;
			this.postal = postal;
			this.city = city;
		}

		public string Street {
			get {
				return street;
			}
			set {
				street = value;
			}
		}

		public int Number {
			get {
				return number;
			}
			set {
				number = value;
			}
		}

		public long Postal {
			get {
				return postal;
			}
			set {
				postal = value;
			}
		}

		public City City {
			get {
				return city;
			}
			set {
				city = value;
			}
		}
	}
}