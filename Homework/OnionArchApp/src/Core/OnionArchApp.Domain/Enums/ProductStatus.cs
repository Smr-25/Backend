using System.ComponentModel;

namespace OnionArchApp.Domain.Enums;

public enum ProductStatus
{
    [Description("None")]
    None = 0,
    [Description("New")]
    New = 1,
    [Description("Featured")]
    Featured = 2,
}