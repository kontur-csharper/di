﻿using MatthiWare.CommandLine.Core.Attributes;

namespace TagsCloudContainer.UserOptions
{
    public class TextProcessingCommands : StorageCommands
    {
        [Required, Name("bw", "boring"), Description("Boring words, space separated between words")]
        public string[] BoringWords { get; set; }
    }
}