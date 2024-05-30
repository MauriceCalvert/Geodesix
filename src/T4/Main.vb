Module Main

    Sub Main()

        Dim gfe As New GeneratorFunctions("DrawingFunctionsMaster.vb", "DrawingFunctions.txt", "..\..\DrawingFunctions.vb")
        gfe.Run()
        Dim gfd As New GeneratorFunctions("ExcelFunctionsMaster.vb", "ExcelFunctions.txt", "..\..\ExcelFunctions.vb")
        gfd.Run()
        Dim gfg As New GeneratorFunctions("GeodesixFunctionsMaster.vb", "GeodesixFunctions.txt", "..\..\GeodesixFunctions.vb")
        gfg.Run()

    End Sub

End Module
