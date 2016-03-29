WORD 2007 CONTENT CONTROL TOOLKIT -- SAMPLE DOCUMENTS
Visit the project at www.codeplex.com/dbe
=====================================================

SUMMARY

This package contains sample documents you can use to explore how content controls and custom xml work together. Because all of the documents have the content controls bound to the appropriate XML nodes, you can use the Word 2007 Content Control Toolkit to see their relationship and test out the binding functionality.

IMPORTANT NOTICE

The documents included in this package are bound by the Ms-LPL license (included in the package as \license.txt). The documents are intended only for demonstration purposes only and are to be used at your own risk. They are not supported by Microsoft.

LEARNING EXERCISE

1. Open an included "complex" or "simple" document in Word
2. Enable the "Developer" tab on the "Ribbon" menu to see the content control menu
3. [optional] Select content controls and view their properties
4. Add some data to a particular content control
5. Save and close Word
6. Open the document in the Word 2007 Content Control Toolkit
7. Observe the content control list on the left
8. Double click on a content control to see all the information associated with it
9. On the right hand side, observe the XML structure of the custom xml document
10. Expand the nodes of the XML "tree view" to reveal child nodes if available
11. Switch to "Edit View" and observe the values you added in step 4
12. Edit the data again to something memorable
13. Add a new node by just adding a new line, say "<foo>zar</foo>"
14. Switch back to "Bind View" and observe the new node "foo"
15. Click on the node "foo" to select it
16. Drag the node to a text content control, such as a name field.
17. Save and close the Content Control toolkit
18. Open Word
19. Observe the changes

NOTES

> Binding image content controls

Note that when you bind an image content control to an XML node, the content inside of the node becomes a base 64 encoded blob of text.

> MedicalChartSample Document:

This document may have display issues with the Winding font used to simulate check boxes. If your machine displays [?] instead of check boxes, then you may consider changing the Winding font to a standard font like Arial and change the display string to something like [] for an empty check box, and [x] for "checked".

The XML structure behind the document conforms to no schema. The structure is inconsistent to demonstrate the types of binding you can do (to attribute, to node, ect).
