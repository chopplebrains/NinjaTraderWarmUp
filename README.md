# NinjaTraderWarmUp

NinjaTrader 8 custom indicators for trading warm-up.

## Indicators

### WarmUpMA — Dual Moving Average with Trend Coloring

Plots a **fast SMA** and a **slow SMA** directly on the price chart.
The fast MA changes color to signal trend direction at a glance:

| Color | Meaning |
|-------|---------|
| 🟢 Green | Fast MA above Slow MA (bullish) |
| 🔴 Red | Fast MA below Slow MA (bearish) |
| 🔵 Blue | Fast MA equals Slow MA (neutral) |
| 🟠 Orange | Slow MA (always orange) |

**Default settings:** Fast = 9, Slow = 21

## Installation

1. Copy `Indicators/WarmUpMA.cs` to:
   ```
   Documents\NinjaTrader 8\bin\Custom\Indicators\
   ```
2. In NinjaTrader 8, go to **Tools → Edit NinjaScript → Compile** (or it compiles automatically on restart).
3. Add the indicator to a chart via **right-click → Indicators → WarmUpMA**.

## Customization

In the indicator properties dialog you can change:
- **Fast Period** — default 9
- **Slow Period** — default 21
