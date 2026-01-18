Namespace Framework.UI.Theme

    ''' <summary>
    ''' アプリケーション全体で使用する配色テーマをまとめたパレットクラス。
    ''' <para>
    ''' ・フォーム背景色  
    ''' ・パネル背景色  
    ''' ・ラベル文字色  
    ''' ・テキストボックスの前景／背景色  
    ''' ・ボタンの前景／背景色  
    ''' ・グリッドヘッダーの前景／背景色  
    ''' </para>
    ''' UI の統一感を保ち、テーマ変更を容易にするための中心的な設定クラス。
    ''' </summary>
    Public Class ThemeColorPalette

        ''' <summary>
        ''' フォーム全体の背景色。
        ''' </summary>
        Public Property FormBackColor As Color = Color.FromArgb(245, 245, 245)

        ''' <summary>
        ''' パネルの背景色。
        ''' </summary>
        Public Property PanelBackColor As Color = Color.White

        ''' <summary>
        ''' ラベルの文字色。
        ''' </summary>
        Public Property LabelForeColor As Color = Color.FromArgb(50, 50, 50)

        ''' <summary>
        ''' テキストボックスの背景色。
        ''' </summary>
        Public Property TextBoxBackColor As Color = Color.White

        ''' <summary>
        ''' テキストボックスの文字色。
        ''' </summary>
        Public Property TextBoxForeColor As Color = Color.Black

        ''' <summary>
        ''' ボタンの背景色。
        ''' </summary>
        Public Property ButtonBackColor As Color = Color.FromArgb(230, 230, 230)

        ''' <summary>
        ''' ボタンの文字色。
        ''' </summary>
        Public Property ButtonForeColor As Color = Color.Black

        ''' <summary>
        ''' グリッドヘッダーの背景色。
        ''' </summary>
        Public Property GridHeaderBackColor As Color = Color.FromArgb(220, 220, 220)

        ''' <summary>
        ''' グリッドヘッダーの文字色。
        ''' </summary>
        Public Property GridHeaderForeColor As Color = Color.Black

    End Class

End Namespace