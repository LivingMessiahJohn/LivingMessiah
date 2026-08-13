namespace RCL.Features.Storage;

/// <summary>
/// Text blob download: body plus last-revised from blob metadata (or LastModified fallback).
/// </summary>
public sealed record BlobTextContent(string Text, DateTime LastRevised);
