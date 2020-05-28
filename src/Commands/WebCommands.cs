using Rocket.API;
using Rocket.Unturned.Player;
using System.Collections.Generic;

namespace papershredder432.PSEssentials.Commands
{
    public class WebCommands : IRocketCommand
    {
        private string name;
        private string help;
        private string url;
        private string description;

        public WebCommands(string name, string description, string url, string cmdHelp)
        {
            name = name;
            help = cmdHelp;
            url = url;
            description = description;

        }

        public AllowedCaller AllowedCaller => AllowedCaller.Player;

        public string Name
        {
            get { return name; }
        }

        public string Help
        {
            get { return help; }
        }

        public string Syntax
        {
            get { return ""; }
        }

        public List<string> Aliases
        {
            get { return new List<string>(); }
        }

        public List<string> Permissions
        {
            get { return new List<string>(); }
        }

        public void Execute(IRocketPlayer caller, string[] command)
        {
            UnturnedPlayer uCaller = (UnturnedPlayer)caller;

            PSEssentials.OpenUrl(uCaller, url, description);
        }
    }
}
