   M S B A M L   `   `   `  ����F  BVideoEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:VideoEditorVideoEditor  5   	   7 clr-namespace:VideoEditor.ModelsVideoEditor.Models  5   	       VideoEditor.MainWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   +localclr-namespace:VideoEditor     5   	   3models clr-namespace:VideoEditor.Models     5   	   -   5          � Title$  Video Viewer��5   	   $	��450��6   $	��800��6+   .��5      � ���	#FF333333 � RowDefinitions 5	   
   �� 5
      $��* �6   �� 5      $
��Auto �6   .+�5   
   �� -   5        $��videoFilesListView��5      $��0��6-   $��10q�6:      CardItemsPanelTemplate#���   6F   ��5      �� 5       �� DataType"   VideoEditor.Models.VideoItem"  6   .R�5      �� 
������� �� CornerRadius$ 5��60    �� Padding$ 10q�6A   $��5q�6N   ._�5      �� .+�5      �� $	��100��6B   $��UniformToFill=�6O   ��6$   �� *
CoverImage+6O   �� 5      $
��Bold"�6>   $��5q�6P   ��6(   �� *Name+6P   �� 5      $
��Wrap=�6>   $��5q�6R   ��6(   �� *Path+6R   5      5      5      5      r� 5   
   -   5        $��videoPlayer��5      $��0��6*   $��Stretch=�5      $��Stretch=�6-    r� LoadedBehavior$ Manual=�5       r� Stretch$ Uniform=�6'   $
|�True��5"      5#      �� 5)   
   -   5        $��pauseOverlay��5)      $��Paused��6(   $y�Hidden=�66   $��0��5*      $��Center=�6   $��Center=�6:   
�������$��36(�5+   #   $
��Bold"�61   ���	#660000005,      �� 5/   
   $��1��6   ���	#FF444444$ 10q�65   $ 0��6B   ._�50      ��  �� Orientation$ 
Horizontal=�6   $��Center=�62   $��Center=�6O   .+�52      �� -   5        $
��Load��52      $��5q�6(   $��5q�63   6?   �� 53      -   5        $��▶ Play��53      $��5q�6*   $��5q�65   6A   �� 54      -   5        $��	⏸ Pause��54      $��5q�6+   $��5q�66   6B   �� 55      -   5        $��⏹ Stop��55      $��5q�6*   $��5q�65   6A   �� 56      -	   5        $��Full Screen��56      6/   �� 57      -
   5        $��Browse Files��57      $��5q�60   $��5q�6;   6G   58      59      5:       � 	Resources 5<      	 �� 
TargetType
 �� Property �� Value �� Triggers P� Property P� Value��%�  '��       �� 5=   
   "	 ��6   .�5>      �� 
 � ��
 �����6+   �� 5?      
 � �� �	#FF5555556+   �� 5@      
 � �� �	#FF7777776,   �� 5A      
 � ��$ 1q�60   �� 5B      
 � ��$
 Bold"�6+   �� 5C      
 � ��$ 80��6&   �� 5D      
 � ��$ 30��6'   �� 5E      
 �@�Cursor$
 Hand��6'    5F      P� 5G       �@J�IsMouseOver$
 True��61   . �5H      �� 
 � �� �	#FF77777763   5I      5K      5L      5M      