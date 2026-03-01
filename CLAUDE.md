# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Repository Overview

Personal fork of [setchi/FancyScrollView](https://github.com/setchi/FancyScrollView) for UPM distribution.
- Package name: `jp.setchi.fancyscrollview`
- Active branch: `upm` (package files only — master is the original upstream dev project and is not used)
- Unity minimum version: 2019.4

## Development Workflow

- Extend code directly on `upm` branch
- Test via local `file:` reference in a separate Unity project (`Packages/manifest.json`: `"jp.setchi.fancyscrollview": "file:<path to this repo>"`)
- Release: commit → push → `git tag vX.Y.Z` → `git push origin vX.Y.Z`

## Architecture

### Class Hierarchy

```
FancyScrollView<TItemData, TContext>          Core scroll logic, cell pooling, loop support
└─ FancyScrollRect<TItemData, TContext>       ScrollRect-style (no loop, no snap, no Unrestricted)
   └─ FancyGridView<TItemData, TContext>      Groups items into rows/columns

FancyCell<TItemData, TContext>                Individual cell base
└─ FancyScrollRectCell<TItemData, TContext>   Adds viewport-relative positioning
   └─ FancyGridViewCell<TItemData, TContext>  Adds grid offset within group
└─ FancyCellGroup<TItemData, TContext>        Container cell wrapping multiple child cells (used by FancyGridView)

Scroller                                     Input handling, inertia, snap, animation
```

### Key Design Points

**Position system:** `FancyScrollView` uses normalized positions (0–1); `Scroller` uses item-index positions (0–totalCount). `FancyScrollRect` bridges them with `ToScrollerPosition()` / `ToFancyScrollViewPosition()`.

**Cell pool:** Cells are reused as the viewport scrolls. `ResizePool()` adjusts pool size; `UpdateCells()` assigns data and visibility each frame. Uniform cell size is assumed.

**Context pattern:** A shared `TContext` object (must be `class, new()`) is passed from the scroll view to all cells. Used to share scroll metrics (size, direction, calculation functions) without tight coupling.

**Loop mode:** Only available in `FancyScrollView` base class. `FancyScrollRect` and `FancyGridView` disable it in `OnValidate()`.

**FancyGridView data flow:** Accepts `IList<TItemData>` and internally groups items into `TItemData[]` arrays of size `startAxisCellCount`, passing each group to a `FancyCellGroup`.

### Easing

`EasingCore.cs` provides 31 easing functions via `Easing.Get(Ease)` returning `EasingFunction` (`float → float`). Used by `Scroller` for `ScrollTo()` animations.

### Editor

`ScrollerEditor.cs` conditionally shows inspector fields: elasticity (only when Elastic), deceleration + snap (only when inertia enabled), using `AnimBool` for smooth transitions.

## Constraints to Be Aware Of

- `FancyScrollRect` / `FancyGridView`: `loop`, `snap`, and `MovementType.Unrestricted` are always disabled (enforced in `OnValidate`)
- Extending `FancyScrollView` directly is needed to use loop or snap
- Custom context types must have a parameterless constructor
