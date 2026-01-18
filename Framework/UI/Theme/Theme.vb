Namespace Framework.UI.Theme

    ''' <summary>
    ''' アプリケーション全体のテーマ設定を保持するクラス。
    ''' <para>
    ''' ・配色設定（<see cref="ThemeColorPalette"/>）  
    ''' ・フォント設定（<see cref="ThemeFontPalette"/>）  
    ''' </para>
    ''' UI の統一感を保ち、画面デザインを一元管理するための基盤となる。
    ''' </summary>
    Public Class Theme

        ''' <summary>
        ''' テーマで使用するカラーセット。
        ''' </summary>
        Public Property Colors As New ThemeColorPalette()

        ''' <summary>
        ''' テーマで使用するフォントセット。
        ''' </summary>
        Public Property Fonts As New ThemeFontPalette()

    End Class

End Namespace