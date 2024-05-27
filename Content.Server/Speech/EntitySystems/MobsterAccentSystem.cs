using System.Linq;
using System.Text.RegularExpressions;
using Content.Server.Speech.Components;
using Robust.Shared.Random;

namespace Content.Server.Speech.EntitySystems;

public sealed class MobsterAccentSystem : EntitySystem
{
    private static readonly Regex RegexIng = new(@"(?<=\w\w)(in)g(?!\w)", RegexOptions.IgnoreCase);
    private static readonly Regex RegexLowerOr = new(@"(?<=\w)o[Rr](?=\w)");
    private static readonly Regex RegexUpperOr = new(@"(?<=\w)O[Rr](?=\w)");
    private static readonly Regex RegexLowerAr = new(@"(?<=\w)a[Rr](?=\w)");
    private static readonly Regex RegexUpperAr = new(@"(?<=\w)A[Rr](?=\w)");
    private static readonly Regex RegexFirstWord = new(@"^(\S+)");
    private static readonly Regex RegexLastWord = new(@"(\S+)$");

    [Dependency] private readonly IRobustRandom _random = default!;
    [Dependency] private readonly ReplacementAccentSystem _replacement = default!;

    private static readonly Dictionary<string, string> DirectReplacements = new()
    {
        { "let me", "lemme" },
        { "should", "oughta" },
        { "the", "da" },
        { "them", "dem" },
        { "attack", "whack" },
        { "kill", "whack" },
        { "murder", "whack" },
        { "dead", "sleepin' with da fishies"},
        { "hey", "ey'o" },
        { "hi", "ey'o"},
        { "hello", "ey'o"},
        { "rules", "roolz" },
        { "you", "yous" },
        { "have to", "gotta" },
        { "going to", "boutta" },
        { "about to", "boutta" },
        { "here", "'ere" },
		
		{ "дай", "дай-ка" },
        { "надо", "надо бы" },
		
        { "это", "эт" },
        { "эта", "эт" },
        { "этот", "эт" },
        { "эти", "эт" },
        { "этого", "этго" },
        { "этому", "этму"},
        { "этим", "этм"},
        { "эту", "эт"},
        { "этих", "этх" },
        { "этой", "этою" },
		
        { "бить", "вальцануть" },
        { "бью", "вальцую" },
        { "бьем", "вальцуем" },
        { "бьём", "вальцуем" },
        { "бил", "вальцевал" },
        { "била", "вальцевала" },
        { "бьешь", "вальцуешь" },
        { "бьёшь", "вальцуешь" },
        { "бей", "вальцуй" },
        { "бьет", "вальцует" },
        { "било", "вальцевало" },
        { "били", "вальцевали" },
        { "бьёте", "вальцуете" },
        { "бьете", "вальцуете" },
        { "бейте", "вальцуйте" },
        { "бьют", "вальцуют" },
        { "бив", "вальцевав" },
		
		{ "убить", "загасить" },
        { "убью", "загашу" },
        { "убьем", "загасим" },
        { "убьём", "загасим" },
        { "убил", "загасил" },
        { "убила", "загасила" },
        { "убьешь", "загасишь" },
        { "убьёшь", "загасишь" },
        { "убей", "гаси" },
        { "убьет", "загасит" },
        { "убило", "загасило" },
        { "убили", "загасили" },
        { "убьёте", "загасите" },
        { "убьете", "загасите" },
        { "убейте", "гасите" },
        { "убьют", "загасят" },
        { "убив", "загасив" },
		
        { "охрана", "затылки" },
		{ "охрану", "затылка" },
		{ "охраны", "затылка" },
		{ "охраной", "затылком" },
		{ "охране", "затылку" },
		{ "охран", "затылков" },
		{ "охранам", "затылкам" },
		{ "охранами", "затылками" },
		
        { "охранник", "затылок" },
		{ "охраннику", "затылку" },
		{ "охранника", "затылка" },
		{ "охранником", "затылком" },
		{ "охраннике", "затылке" },
		{ "охранников", "затылков" },
		{ "охранникам", "затылкам" },
		{ "охранниках", "затылках" },
		{ "охранниками", "затылками" },
		
        { "безопасник", "затылок" },
		{ "безопаснику", "затылку" },
		{ "безопасника", "затылка" },
		{ "безопасником", "затылком" },
		{ "безопаснике", "затылке" },
		{ "безопасников", "затылков" },
		{ "безопасникам", "затылкам" },
		{ "безопасниках", "затылках" },
		{ "безопасниками", "затылками" },
		
		{ "лох", "лепила" },
		{ "дурак", "лепила" },
		
        { "конфликт", "мутиловка" },
        { "драка", "мутиловка" },
        { "наезд", "мутиловка" },
        { "бой", "мутиловка" },
		
        { "наглый", "отмороженный" },
        { "тупой", "отмороженный" },
		
        { "вопрос", "предъява" },
		
        { "бандит", "торпеда" },
		
        { "место", "точка" },
        { "зона", "точка" },
        { "группа", "хоровод" },
        { "мертв", "трупак" },
		
        { "привет", "атас" },
        { "эй", "атас" },
        { "здравствуйте", "атас" },
        { "хай", "атас" },
		
        { "деньги", "бабки" },
        { "контрабандист", "авиатор" },
        { "шаттл", "телега" },
        { "корабль", "телега" },
        { "порядок", "ажур" },
        { "товарищ", "леха" },
        { "бутылка", "ампула" },
        { "мензурка", "ампула" },
        { "голова", "арбуз" },
        { "кружка", "бадья" },
        { "молчать", "базар на ноль" },
        { "отвлечь", "баки забить" },
        { "допрос", "баня" },
        { "наручники", "баранки" },
        { "детектив", "барбос" },
        { "смотритель", "барин" },
        { "глава", "батя" },
        { "язык", "ботало" },
        { "нож", "жало" },
        { "карцер", "кандей" },
        { "сломать", "раздербанить" },
		
		
        { "зарезать", "проучить" },
        { "отряд", "хоровод" }
    };

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<MobsterAccentComponent, AccentGetEvent>(OnAccentGet);
    }

    public string Accentuate(string message, MobsterAccentComponent component)
    {
        // Order:
        // Do text manipulations first
        // Then prefix/suffix funnyies

        // direct word replacements
        var msg = _replacement.ApplyReplacements(message, "mobster");

        // thinking -> thinkin'
        // king -> king
        //Uses captures groups to make sure the captialization of IN is kept
        msg = RegexIng.Replace(msg, "$1'");

        // or -> uh and ar -> ah in the middle of words (fuhget, tahget)
        msg = Regex.Replace(msg, @"(?<=\w)or(?=\w)", "бля", RegexOptions.IgnoreCase);
        msg = Regex.Replace(msg, @"(?<=\w)ar(?=\w)", "ска", RegexOptions.IgnoreCase);
        msg = Regex.Replace(msg, @"(?<=\w)ar(?=\w)", "нах", RegexOptions.IgnoreCase);

        // Prefix
        if (_random.Prob(0.15f))
        {
            //Checks if the first word of the sentence is all caps
            //So the prefix can be allcapped and to not resanitize the captial
            var firstWordAllCaps = !RegexFirstWord.Match(msg).Value.Any(char.IsLower);
            var pick = _random.Next(1, 2);

            // Reverse sanitize capital
            var prefix = Loc.GetString($"accent-mobster-prefix-{pick}");
            if (!firstWordAllCaps)
                msg = msg[0].ToString().ToLower() + msg.Remove(0, 1);
            else
                prefix = prefix.ToUpper();
            msg = prefix + " " + msg;
        }

        // Sanitize capital again, in case we substituted a word that should be capitalized
        msg = msg[0].ToString().ToUpper() + msg.Remove(0, 1);

        // Suffixes
        if (_random.Prob(0.4f))
        {
            //Checks if the last word of the sentence is all caps
            //So the suffix can be allcapped
            var lastWordAllCaps = !RegexLastWord.Match(msg).Value.Any(char.IsLower);
            var suffix = "";
            if (component.IsBoss)
            {
                var pick = _random.Next(1, 4);
                suffix = Loc.GetString($"accent-mobster-suffix-boss-{pick}");
            }
            else
            {
                var pick = _random.Next(1, 3);
                suffix = Loc.GetString($"accent-mobster-suffix-minion-{pick}");
            }
            if (lastWordAllCaps)
                suffix = suffix.ToUpper();
            msg += suffix;
        }

        return msg;
    }

    private void OnAccentGet(EntityUid uid, MobsterAccentComponent component, AccentGetEvent args)
    {
        args.Message = Accentuate(args.Message, component);
    }
}
