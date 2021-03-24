Partial Class PathListDataSet
    Partial Class PathListDataTable

        Private Sub PathListDataTable_ColumnChanging(sender As System.Object, e As System.Data.DataColumnChangeEventArgs) Handles Me.ColumnChanging
            If (e.Column.ColumnName = Me.PathListIDColumn.ColumnName) Then
                'Add user code here
            End If

        End Sub

    End Class

End Class

Namespace PathListDataSetTableAdapters

    Partial Public Class PathListTableAdapter
    End Class
End Namespace
