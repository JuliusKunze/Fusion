﻿Public Class TextOnlyDocumentTests

    <Test()>
    Public Sub Test()
        Assert.AreEqual(New TextOnlyDocument(New FlowDocument(New Paragraph(New Run("ABC")))).GetTextRange(0, 1).Text, "A")
        Assert.AreEqual(New TextOnlyDocument(New FlowDocument(New Paragraph(New Run(" ABC")))).GetTextRange(0, 1).Text, " ")
        Assert.AreEqual(New TextOnlyDocument(New FlowDocument(New Paragraph(New Run("ABC")))).GetTextRange(0, 3).Text, "ABC")
        Assert.AreEqual(New TextOnlyDocument(New FlowDocument(New Paragraph(New Run(" ABC")))).GetTextRange(1, 2).Text, "AB")
    End Sub

    <Test()>
    Public Sub Test2()
        Dim document = New FlowDocument

        document.Blocks.Add(New Paragraph(New Run("A")))

        Dim block2 = New Paragraph
        block2.Inlines.Add(New Run("B2"))
        block2.Inlines.Add(New Run("C33"))

        document.Blocks.Add(block2)

        Assert.AreEqual(New TextOnlyDocument(document).GetTextRange(3, 2).Text, "B2")
        Assert.AreEqual(New TextOnlyDocument(document).GetTextRange(5, 2).Text, "C3")
    End Sub

    <Test()>
    Public Sub Test3()
        Dim document = New FlowDocument

        document.Blocks.Add(New Paragraph())
        document.Blocks.Add(New Paragraph(New Run("Test")))

        Assert.AreEqual(New TextOnlyDocument(document).GetTextRange(2, 4).Text, "Test")
    End Sub

    <Test()>
    Public Sub TestGetIndex()
        Dim document = New FlowDocument

        document.Blocks.Add(New Paragraph())
        document.Blocks.Add(New Paragraph(New Run("Test")))

        Dim textPointer = CType(CType(document.Blocks(1), Paragraph).Inlines(0), Run).ContentStart.GetPositionAtOffset(2)

        Dim textOnlyDocument = New TextOnlyDocument(document)

        Assert.AreEqual(textOnlyDocument.GetIndex(textPointer), 4)
    End Sub

End Class