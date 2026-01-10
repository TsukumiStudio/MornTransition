# MornTransition

画面遷移エフェクトを管理するライブラリ

## 依存関係

- UniTask
- MornGlobal
- MornEnum

## セットアップ

1. `Project`を右クリック → `Morn/MornTransitionGlobal`を作成
2. `TransitionNames`にトランジション名を設定
3. シーンに`MornTransitionCtrl`を配置
4. 子オブジェクトにトランジションコンポーネントを追加

## 使い方

```csharp
// 画面を覆う（フェードイン）
await transitionCtrl.FillAsync(transitionType, ct);

// 画面を表示（フェードアウト）
await transitionCtrl.ClearAsync(ct);

// 即時実行版
transitionCtrl.FillImmediate(transitionType);
transitionCtrl.ClearImmediate();

// 現在覆われているか
bool isFilling = transitionCtrl.IsFilling();
```

## トランジション種類

| コンポーネント | 説明 |
|---|---|
| `MornTransitionFade` | Imageのアルファ値でフェード |
| `MornTransitionAnimation` | Animatorでアニメーション再生 |
| `MornTransitionMaterial` | シェーダーパラメータでトランジション |

## カスタムトランジション

`MornTransitionBase`を継承して独自のトランジションを実装可能
