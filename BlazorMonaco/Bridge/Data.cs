﻿using System;
using System.Collections.Generic;
using BlazorMonaco.Extensions;

namespace BlazorMonaco
{
    public class LanguageConfiguration
    {
        public CommentRule? Comments { get; set; }
        public string[][]? Brackets { get; set; } // CharacterPair[]? -> [string, string];
        public string? WordPattern { get; set; }
        public IndentationRule? IndentationRules { get; set; }
        public OnEnterRule[]? OnEnterRules { get; set; }
        public AutoClosingPairConditional[]? AutoClosingPairs { get; set; }
        public AutoClosingPair[]? SurroundingPairs { get; set; } // CharacterPair[]? -> [string, string];
        public string[][]? ColorizedBracketPairs { get; set; }
        public string? AutoCloseBefore { get; set; }
        public FoldingRules? Folding { get; set; }
    }

    public class IndentationRule
    {
        public string DecreaseIndentPattern { get; set; }
        public string IncreaseIndentPattern { get; set; }
        public string? IndentNextLinePattern { get; set; }
        public string? UnIndentedLinePattern { get; set; }
    }

    public class OnEnterRule
    {
        public string BeforeText { get; set; }   
        public string? AfterText { get; set; }   
        public string? PreviousLineText { get; set; }   
        public EnterAction Action { get; set; }   
    }

    public class CommentRule
    {
        public string? LineComment { get; set; }
        public string[]? BlockComment { get; set; }
    }
    
    public interface IAutoClosingPair
    {
        public string Open { get; set; }
        public string? Close { get; set; }
    }
    
    public class AutoClosingPairConditional : IAutoClosingPair
    {
        public string[]? NotIn { get; set; }
        public string Open { get; set; }
        public string? Close { get; set; }
    }
    
    public class AutoClosingPair : IAutoClosingPair
    {
        public string Open { get; set; }
        public string? Close { get; set; }
    }

    public class FoldingMarkers
    {
        public string Start { get; set; }
        public string End { get; set; }
    }
    
    public class FoldingRules
    {
        public bool? OffSide { get; set; }
        public FoldingMarkers? Markers { get; set; }
    }

    public class EnterAction
    {
        public IndentAction IndentAction { get; set; }
        public string? AppendText { get; set; }
        public int? RemoveText { get; set; }
    }

    public interface IMonarchToken
    {

    }

    public class MonarchCase
    {
        public string Token { get; set; }
        public string Type { get; set; }

        public MonarchCase(string token, string type)
        {
            Token = token;
            Type = type.ToMonarchType();
        }
    }

    public class MonarchTokenValue : IMonarchToken
    {
        public object Value { get; set; }

        public MonarchTokenValue(object value)
        {
            Value = value;
        }

        public static implicit operator string(MonarchTokenValue monarchRuleValue) => monarchRuleValue.Value.ToString();
        public static implicit operator CaseToken(MonarchTokenValue monarchRuleValue) => monarchRuleValue.Value as CaseToken;
        public static implicit operator ConditionToken(MonarchTokenValue monarchRuleValue) => monarchRuleValue.Value as ConditionToken;
        public static explicit operator MonarchTokenValue(string b) => new(b);
        public static explicit operator MonarchTokenValue(CaseToken b) => new(b);
        public static explicit operator MonarchTokenValue(ConditionToken b) => new(b);

        public override string ToString() => $"{Value}";
    }

    public class CaseToken : IMonarchToken
    {
        public List<MonarchCase> Cases { get; set; }
    }

    public class ConditionToken : IMonarchToken
    {
        public string Token { get; set; }
        public string Log { get; set; }
        public string Bracket { get; set; }
        public string Next { get; set; }
    }

    public class MonarchRoot
    {
        public string Pattern { get; set; }
        public MonarchTokenValue Token { get; set; }
    }

    public class TokenizerData
    {
        public List<MonarchRoot> Root { get; set; }
    }

    public class MonarchLanguage
    {
        public string Id { get; set; }
    }

    public class MonarchLanguageRules
    {
        public List<string> Keywords { get; set; }
        public List<string> TypeKeywords { get; set; }
        public List<string> Operators { get; set; }
        public TokenizerData Tokenizer { get; set; }
    }

    public class Position
    {
        public int LineNumber { get; set; }
        public int Column { get; set; }
    }

    public class Range
    {
        public int StartLineNumber { get; set; }
        public int StartColumn { get; set; }
        public int EndLineNumber { get; set; }
        public int EndColumn { get; set; }

        public Range() { }

        public Range(int startLineNumber, int startColumn, int endLineNumber, int endColumn)
        {
            StartLineNumber = startLineNumber;
            StartColumn = startColumn;
            EndLineNumber = endLineNumber;
            EndColumn = endColumn;
        }
    }

    public class Selection : Range
    {
        public int SelectionStartLineNumber { get; set; }
        public int SelectionStartColumn { get; set; }
        public int PositionLineNumber { get; set; }
        public int PositionColumn { get; set; }
    }

    public class Dimension
    {
        public int Width { get; set; }
        public int Height { get; set; }
    }

    public class StandaloneThemeData
    {
        public string Base { get; set; }
        public bool? Inherit { get; set; }
        public List<TokenThemeRule> Rules { get; set; }
        public List<string> EncodedTokensColors { get; set; }
        public Dictionary<string, string> Colors { get; set; }
    }

    public class TokenThemeRule
    {
        public string Token { get; set; }
        public string Foreground { get; set; }
        public string Background { get; set; }
        public string FontStyle { get; set; }
    }

    public class EditorLayoutInfo
    {
        public double Width { get; set; }
        public double Height { get; set; }
        public double GlyphMarginLeft { get; set; }
        public double GlyphMarginWidth { get; set; }
        public double LineNumbersLeft { get; set; }
        public double LineNumbersWidth { get; set; }
        public double DecorationsLeft { get; set; }
        public double DecorationsWidth { get; set; }
        public double ContentLeft { get; set; }
        public double ContentWidth { get; set; }
        public EditorMinimapLayoutInfo Minimap { get; set; }
        public double ViewportColumn { get; set; }
        public bool IsWordWrapMinified { get; set; }
        public bool IsViewportWrapping { get; set; }
        public double WrappingColumn { get; set; }
        public double VerticalScrollbarWidth { get; set; }
        public double HorizontalScrollbarHeight { get; set; }
        public OverviewRulerPosition OverviewRuler { get; set; }
    }

    public class EditorMinimapLayoutInfo
    {
        public RenderMinimap RenderMinimap { get; set; }
        public double MinimapLeft { get; set; }
        public double MinimapWidth { get; set; }
        public bool MinimapHeightIsEditorHeight { get; set; }
        public bool MinimapIsSampling { get; set; }
        public double MinimapScale { get; set; }
        public double MinimapLineHeight { get; set; }
        public double MinimapCanvasInnerWidth { get; set; }
        public double MinimapCanvasInnerHeight { get; set; }
        public double MinimapCanvasOuterWidth { get; set; }
        public double MinimapCanvasOuterHeight { get; set; }
    }

    public class OverviewRulerPosition
    {
        public double Width { get; set; }
        public double Height { get; set; }
        public double Top { get; set; }
        public double Right { get; set; }
    }

    public class Marker
    {
        public object Code { get; set; }

        public string Message { get; set; }

        public MarkerSeverity Severity { get; set; }

        public int StartLineNumber { get; set; }

        public int StartColumn { get; set; }

        public int EndLineNumber { get; set; }

        public int EndColumn { get; set; }

        public string Source { get; set; }

        public MarkerTag[] Tags { get; set; } = Array.Empty<MarkerTag>();
    }

    public class MarkerCode
    {
        public Uri Target { get; set; }

        public string Value { get; set; }
    }

    public class CodeAction
    {
        public string Title { get; set; }

        public string Kind { get; set; }

        public Marker[] Diagnostics { get; set; } = Array.Empty<Marker>();

        public WorkspaceEdit Edit { get; } = new WorkspaceEdit();

        public bool IsPreferred { get; set; }
    }

    public class WorkspaceEdit
    {
        public WorkspaceTextEdit[] Edits { get; set; } = Array.Empty<WorkspaceTextEdit>();
    }

    public class WorkspaceTextEdit
    {
        public string Resource { get; set; }

        public TextEdit Edit { get; } = new TextEdit();
    }

    public class TextEdit
    {
        public string Text { get; set; }

        public Range Range { get; set; }

        public EndOfLineSequence? Eol { get; set; }
    }

    public class CompletionItem
    {
        public string Label { get; set; }

        public Range Range { get; set; }

        public string Detail { get; set; }

        public CompletionItemKind Kind { get; set; }

        public string InsertText { get; set; }

        public bool Preselect { get; set; }
    }
}
