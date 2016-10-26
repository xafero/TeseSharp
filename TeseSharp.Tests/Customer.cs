using System;
using System.Numerics;

namespace TeseSharp.Tests
{
	public class Customer
	{
		private long id;

		private string firstName;

		private string lastName;

		private double money;

		private bool male;

		private Address home;

		private char sex;

		private int houses;

		private short pets;

		private float crazyness;

		private byte bits;

		private BigInteger sleep;

		private decimal awake;

		private DateTime birth;

		public Customer()
		{
		}

		public Customer(long id, string firstName, string lastName, double money, bool male, 
			char sex, int houses, short pets, float crazyness, byte bits,
			BigInteger sleep,	decimal awake, DateTime birth, Address home)
		{
			this.id = id;
			this.firstName = firstName;
			this.lastName = lastName;
			this.money = money;
			this.male = male;
			this.sex = sex;
			this.houses = houses;
			this.pets = pets;
			this.crazyness = crazyness;
			this.bits = bits;
			this.sleep = sleep;
			this.awake = awake;
			this.birth = birth;
			this.home = home;
		}

		public long Id {
			get {
				return id;
			}
			set {
				id = value;
			}
		}

		public string FirstName {
			get {
				return firstName;
			}
			set {
				firstName = value;
			}
		}

		public string LastName {
			get {
				return lastName;
			}
			set {
				lastName = value;
			}
		}

		public double Money {
			get {
				return money;
			}
			set {
				money = value;
			}
		}

		public bool Male {
			get {
				return male;
			}
			set {
				male = value;
			}
		}

		public Address Home {
			get {
				return home;
			}
			set {
				home = value;
			}
		}

		public char Sex {
			get {
				return sex;
			}
			set {
				sex = value;
			}
		}

		public int Houses {
			get {
				return houses;
			}
			set {
				houses = value;
			}
		}

		public short Pets {
			get {
				return pets;
			}
			set {
				pets = value;
			}
		}

		public float Crazyness {
			get {
				return crazyness;
			}
			set {
				crazyness = value;
			}
		}

		public byte Bits {
			get {
				return bits;
			}
			set {
				bits = value;
			}
		}

		public BigInteger Sleep {
			get {
				return sleep;
			}
			set {
				sleep = value;
			}
		}

		public decimal Awake {
			get {
				return awake;
			}
			set {
				awake = value;
			}
		}

		public DateTime Birth {
			get {
				return birth;
			}
			set {
				birth = value;
			}
		}
	}
}