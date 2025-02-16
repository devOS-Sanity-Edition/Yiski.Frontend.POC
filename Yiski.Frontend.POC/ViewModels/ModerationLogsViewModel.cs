using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Yiski.Frontend.POC.ViewModels;

public class ModerationLogsViewModel : ViewModelBase {
    public ObservableCollection<ModerationLogItem> ModerationEntries { get; }

    public ModerationLogsViewModel() {
        var entries = new List<ModerationLogItem> {
            new ModerationLogItem {
                InfractionId = "1ec26b30-b6b7-4452-a42d-f7e32cbfe8b4",
                GuildId = 942513671723180034,
                UserId = 81062087257755648,
                InfractionType = InfractionType.BAN,
                Reason = "embeddium affiliation and fucking cringe",
                ModeratorId = 942533628435501076,
                CreatedAt = 1728180232700
            },
            new ModerationLogItem {
                InfractionId = "7e1030ab-64e7-47dd-b932-0dbf653e351e",
                GuildId = 942513671723180034,
                UserId = 930222347166818404,
                InfractionType = InfractionType.BAN,
                Reason = "do i really need to say anything, we can all guess lmao",
                ModeratorId = 942533628435501076,
                CreatedAt = 1728180397529
            },
            new ModerationLogItem {
                InfractionId = "950f4477-104b-458e-981e-d03ca340f407",
                GuildId = 942513671723180034,
                UserId = 109040264529608704,
                InfractionType = InfractionType.BAN,
                Reason = "what the fuck is this video https://cdn.discordapp.com/attachments/1124489713886167122/1297938374941872299/bark-bark.mp4?ex=6717becf&is=67166d4f&hm=62908fbbb05e68e15a185c29d77103a1e6a006311183746de523010ba9727c05&",
                ModeratorId = 942533628435501076,
                CreatedAt = 1729523905277
            },
            new ModerationLogItem {
                InfractionId = "0e89ebcf-1b4c-4329-b18b-01fd4642f80e",
                GuildId = 942513671723180034,
                UserId = 580121004739788809,
                InfractionType = InfractionType.BAN,
                Reason = "thingy",
                ModeratorId = 968666256683196427,
                RoleIDs = [942529312442175500, 953095283074551808],
                CreatedAt = 1738959684272
            }
        };

        ModerationEntries = new ObservableCollection<ModerationLogItem>(entries);
    }
}

public enum InfractionType {
    NOTE = 0,
    WARN = 1,
    TIMEOUT = 2,
    ERROR = 3,
    KICK = 4,
    BAN = 5,
    UNBAN = 6
}

public class ModerationLogItem {
    public required string InfractionId { set; get; }
    public required long GuildId { set; get; }
    public required long UserId { set; get; }
    public required InfractionType InfractionType { set; get; }
    public string Reason { set; get; }
    public required long ModeratorId { set; get; }
    public string[] Messages { set; get; }
    public long[] RoleIDs { set; get; }
    public required long CreatedAt { set; get; }
    public long Duration { set; get; }
    public long EndTime { set; get; }
}