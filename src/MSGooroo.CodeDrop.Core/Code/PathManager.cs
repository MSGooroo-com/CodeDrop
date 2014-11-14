using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace MSGooroo.CodeDrop {
	/// <summary>
	/// Summary description for PathManager
	/// </summary>
	public static class PathManager {
		public static string ConfigPath {
			get {
				return System.IO.Path.Combine(new string[] { Path, "config" });
			}
		}

		public static string BatchPath {
			get {
				return System.IO.Path.Combine(new string[] { Path, "batch" });
			}
		}
		public static string ConfigFile {
			get {
				return System.IO.Path.Combine(new string[] { ConfigPath, "config.json" });
			}
		}
		public static string SitesFile {
			get {
				return System.IO.Path.Combine(new string[] { ConfigPath, "sites.json" });
			}
		}

		public static string Path {
			get {
				// Walk the current path backwards until we find a "config" directory
				DirectoryInfo d = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
				DirectoryInfo config = null;
				while (d != null && config == null) {
					config = d.GetDirectories().FirstOrDefault(x => x.Name == "config");
					d = d.Parent;
				}
				if (d == null || config.Parent == null) {
					throw new Exception("Unable to find \"config\" path, quitting.");
				}
				return config.Parent.FullName;
			}
		}
		private static bool _checkedLogPath = false;


		public static string LogPath {
			get {

				string path = System.IO.Path.Combine(new string[] { Path, "log" });
				if (!_checkedLogPath) {
					if (!Directory.Exists(path)) {
						Directory.CreateDirectory(path);
					}
					_checkedLogPath = true;
				}
				return path;
			}
		}



	}
}
