Namespace Framework.Data

    ''' <summary>
    ''' テーブル名およびカラム名を一元管理するための基底クラス。
    ''' 各テーブル定義クラスはこのクラスを継承し、テーブル名を提供する。
    ''' </summary>
    Public MustInherit Class TableDefinitionBase
        ''' <summary>
        ''' テーブル名を返す抽象プロパティ。
        ''' 派生クラスで具体的なテーブル名を実装する。
        ''' </summary>
        Public MustOverride ReadOnly Property Name As String
    End Class

    ' ============================
    ' Users テーブル
    ' ============================

    ''' <summary>
    ''' Users テーブルの定義クラス。
    ''' テーブル名およびカラム名を定数として提供する。
    ''' </summary>
    Public NotInheritable Class UsersTable
        Inherits TableDefinitionBase

        ''' <summary>
        ''' Users テーブル名。
        ''' </summary>
        Public Overrides ReadOnly Property Name As String
            Get
                Return "Users"
            End Get
        End Property

        ''' <summary>
        ''' Users テーブルのカラム名一覧。
        ''' </summary>
        Public NotInheritable Class Columns
            Public Const UserId As String = "UserId"
            Public Const UserName As String = "UserName"
            Public Const Email As String = "Email"
            Public Const CreatedAt As String = "CreatedAt"
            Public Const UpdatedAt As String = "UpdatedAt"
        End Class

    End Class

    ' ============================
    ' Orders テーブル
    ' ============================

    ''' <summary>
    ''' Orders テーブルの定義クラス。
    ''' テーブル名およびカラム名を定数として提供する。
    ''' </summary>
    Public NotInheritable Class OrdersTable
        Inherits TableDefinitionBase

        ''' <summary>
        ''' Orders テーブル名。
        ''' </summary>
        Public Overrides ReadOnly Property Name As String
            Get
                Return "Orders"
            End Get
        End Property

        ''' <summary>
        ''' Orders テーブルのカラム名一覧。
        ''' </summary>
        Public NotInheritable Class Columns
            Public Const OrderId As String = "OrderId"
            Public Const UserId As String = "UserId"
            Public Const Amount As String = "Amount"
            Public Const OrderDate As String = "OrderDate"
        End Class

    End Class

    ' ============================
    ' Products テーブル
    ' ============================

    ''' <summary>
    ''' Products テーブルの定義クラス。
    ''' テーブル名およびカラム名を定数として提供する。
    ''' </summary>
    Public NotInheritable Class ProductsTable
        Inherits TableDefinitionBase

        ''' <summary>
        ''' Products テーブル名。
        ''' </summary>
        Public Overrides ReadOnly Property Name As String
            Get
                Return "Products"
            End Get
        End Property

        ''' <summary>
        ''' Products テーブルのカラム名一覧。
        ''' </summary>
        Public NotInheritable Class Columns
            Public Const ProductId As String = "ProductId"
            Public Const ProductName As String = "ProductName"
            Public Const Price As String = "Price"
        End Class

    End Class

End Namespace