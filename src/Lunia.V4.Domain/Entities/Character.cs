namespace Lunia.V4.Domain.Entities;

public sealed record Item
{
    public string CharacterName { get; set; } = string.Empty;
    public int ItemHash { get; set; }
    public bool IsOnBank { get; set; }
    public int BagNumber { get; set; }
    public int PositionNumber { get; set; }
    public int StackedCount { get; set; }
    public long? ItemSerial { get; set; }
    public long Instance { get; set; }
}

public sealed record Character
{
    public string AccountName { get; set; } = string.Empty;
    public string CharacterName { get; set; } = string.Empty;
    public int VirtualIdCode { get; set; } = 1;
    public int ClassNumber { get; set; }
    public int StageLevel { get; set; } = 1;
    public int StageExp { get; set; }
    public int PvpLevel { get; set; } = 1;
    public int PvpExp { get; set; }
    public int WarLevel { get; set; } = 1;
    public int WarExp { get; set; }
    public long GameMoney { get; set; }
    public long BankMoney { get; set; }
    public int SkillPoint { get; set; }
    public int ExtraBagCount { get; set; }
    public int ExtraBankBagCount { get; set; }
    public int InstantStateFlags { get; set; }
    public int GmLevel { get; set; }
    public bool IsDeleted { get; set; }
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? LastLoggedAt { get; set; }
}