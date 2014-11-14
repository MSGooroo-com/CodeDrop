using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace MSGooroo.CodeDrop {

	public class LogWriter {

		private List<StringBuilder> _streams;

		public LogWriter() {
			_streams = new List<StringBuilder>();
        }

		public void AddStream(StringBuilder watcher) {
			_streams.Add(watcher);
        }

		public void WriteMessage(string message) {
			var entry = string.Format("{0} {1}	msg	{2}",
				DateTime.Now.ToShortDateString(),
				DateTime.Now.ToShortTimeString(),
				message
			);

			foreach (var stream in _streams) {
				stream.AppendLine(entry);
            }

            Console.WriteLine(entry);

		}
		public void WriteError(string message) {
			var entry = string.Format("{0} {1}	error	{2}",
				DateTime.Now.ToShortDateString(),
				DateTime.Now.ToShortTimeString(),
				message
			);

			foreach (var stream in _streams) {
				stream.AppendLine(entry);
			}
			Console.BackgroundColor = ConsoleColor.Red;
			Console.ForegroundColor = ConsoleColor.White;
			Console.WriteLine(entry);
			Console.ResetColor();

		}
	}

}
