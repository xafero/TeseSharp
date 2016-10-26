using System;

namespace TeseSharp.Tests
{
	public class City
	{
		private string name;

		private State state;

		private long code;

		public City()
		{
		}

		public City(string name, State state, long code)
		{
			this.name = name;
			this.state = state;
			this.code = code;
		}

		public string Name {
			get {
				return name;
			}
			set {
				name = value;
			}
		}

		public State State {
			get {
				return state;
			}
			set {
				state = value;
			}
		}

		public long Code {
			get {
				return code;
			}
			set {
				code = value;
			}
		}
	}
}