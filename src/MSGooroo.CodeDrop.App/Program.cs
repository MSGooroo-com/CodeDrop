using System;
using MSGooroo.CodeDrop;

namespace MSGooroo.CodeDrop.App {
	public class Program {
		private readonly IServiceProvider _hostServiceProvider;

		public Program(IServiceProvider hostServiceProvider) {
			_hostServiceProvider = hostServiceProvider;
		}

		public void Main(string[] args) {
			if (args.Length >= 1) {
				var deployKey = args[0];

				var log = Dropper.Deploy(deployKey);
				Console.Write(log);


			} else {
				Console.WriteLine("Error, expected parameter \"deployKey\"");

			}


		}
	}
}

