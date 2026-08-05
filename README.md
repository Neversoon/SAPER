![image](https://github.com/user-attachments/assets/01db1dba-30d3-460e-8a13-88c920db0284)

# SAPER

Simple Minesweeper game built with Unity.

## Overview

This project is a 2D Minesweeper clone with multiple difficulty modes, intuitive input handling, and responsive camera behavior.

## Features

- Easy, Medium, and Hard game modes
- Tap or click to open cells
- Long press (mobile) or secondary action to place or remove flags
- Drag to pan the board
- Pinch or scroll to zoom the board
- Timer and flag counter UI
- Win/lose notifications with sound effects

## Controls

### Desktop

- Primary action (click/tap): open a cell
- Secondary action / long press: place or remove a flag
- Drag: move the board
- Scroll: zoom in and out

### Mobile

- Tap: open a cell
- Long press: place or remove a flag
- Drag: move the board
- Pinch: zoom in and out

## Setup

### Unity Version

This project is configured for Unity `2024.0.5f1`.

### Open in Unity

1. Open Unity Hub.
2. Add the `SAPER` project folder.
3. Open the project.
4. Open the main scene and verify all references in the inspector.

## Project Structure

- `Assets/Scripts/Game/`: game flow, modes, events, timing, and rules.
- `Assets/Scripts/Board/`: runtime board generation and board controller.
- `Assets/Scripts/Cell/`: cell states, cell view updates, and cell logic.
- `Assets/Scripts/Input/`: unified desktop and mobile input handling.
- `Assets/Scripts/UI/`: user interface management and event-driven UI updates.

## Fixes and Improvements

- Corrected cell sprite handling so empty cells no longer keep stale bomb or flag visuals.
- Prevented camera drag state from blocking input after dragging ends.
- Added proper cleanup of mobile and UI input handlers on restart.
- Clamped mine count to valid board dimensions during board generation.
- Added event unsubscription to avoid duplicate event listeners after UI teardown.

## Notes

- The board uses `Assets/Resources/Prefabs/Cell` at runtime to spawn tile objects.
- Assign all serialized fields in the Unity inspector before running.
