using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Newtonsoft.Json;

namespace MSGooroo.CodeDrop {
	public static class Dropper {
		public static string Deploy(string deployKey) {
			
			Console.WriteLine("Running in: " + PathManager.Path);

			var log = new LogWriter();

			StringBuilder builder = new StringBuilder();
			//using (var stream = new StringWriter(builder)) {
			log.AddStream(builder);

			List<SiteConfig> sites = null;
			string sitesJson = File.ReadAllText(PathManager.SitesFile);
			try {
				sites = JsonConvert.DeserializeObject<List<SiteConfig>>(sitesJson);
			} catch (Exception ex) {
				log.WriteError(string.Format("Unable to read sites, json invald: {0}", ex.Message));
				return builder.ToString();
			}

			// Find the site with this identifier...
			var site = sites.FirstOrDefault(x => x.DeployKey == deployKey);
			if (site != null) {
				// Do the deployment....
				site.Initialize();

				var rev = Git.Update(site, log);

				if (site.ProjectFile.EndsWith("project.json")) {
					Deployer.DeployVNext(site, rev, log);
				} else {
					if (Builder.Build(site, log)) {
						log.WriteMessage("Build succeeded, deploying classic site");
						if (Deployer.DeployClassic(site, rev, log)) {
							log.WriteMessage("Deployment Successful!");
						}
					} else {
						log.WriteMessage("Build failed");
					}
				}
			} else {
				log.WriteError(string.Format("Unable to find site with DeployKey: {0}", deployKey));
			}
			return builder.ToString();


		}
	}
}