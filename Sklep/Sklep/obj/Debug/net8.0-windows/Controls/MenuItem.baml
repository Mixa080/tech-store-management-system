   M S B A M L   `   `   `  ����@  <Sklep, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:Sklep.ControlsSklep.Controls  5          Sklep.Controls.MenuItem   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      .localclr-namespace:Sklep.Controls     5      -   5        $
��this��5        � 	Resources  5         IndicatorStyle �� 
TargetType �� Property �� Value  PrimaryBlueColor �� Triggers �� Binding �� RelativeSource �� Mode �� AncestorType	 �� Value  TextFourthColor  MenuIconStyle  SecundaryIconColor  MenuTextStyle  MenuButtonStyle  BorderThickness
 �� 
TargetType �� Padding �� ElementName � ColumnDefinitions �� Stretch  DefaultIcon �� FallbackValue �� TargetNullValue �� Triggers��%|
  &	        &	 �    &	 ;    &	 U    7   7  7  7  7  �� 5   
   " ��6'   .�5      ��  �@��CornerRadius$ 0 5 5 0��6-   �� 5       � ��# �  6+   �� 5       � y�$ Hidden=�6+   �� 5       � ��$ 35��6'   �� 5       � ��$ 4��6&   �� 5       � ��$
 Left=�64   �� 5       � ��$ Center=�62    5      �� 5       6   �� *IsMouseOver+ �� $ FindAncestor=� ToggleButton	 True6�   .a�5      ��  � y�$ Visible=�63   �� 5       � ��# �  63   5      �� 5       6   �� *	IsChecked+ �� $ FindAncestor=� ToggleButton	 True6�   .a�5      ��  � y�$ Visible=�63   �� 5       � ��# �  63   5      5      �� 5!   
   " F�6&   .�5"      ��  � ��# �  6%    5#      �� 5$       6   �� *IsMouseOver+ �� $ FindAncestor=� ToggleButton	 True6�   .a�5%      ��  � ��# �  6-   5&      �� 5'       6   �� *	IsChecked+ �� $ FindAncestor=� ToggleButton	 True6�   .a�5(      ��  � ��# �  6-   5)      5+      �� 5/   
   " ��6&   .�50      ��  � ��# �  6+   �� 51       � ��$ Center=�62   �� 52       � ��$ 12(�6)   �� 53       � ��$ Regular"�6+   �� 54       � ��$ 18 0 0 0q�6'    55      �� 56       6   �� *IsMouseOver+ �� $ FindAncestor=� ToggleButton	 True6�   .a�57      ��  � ��# �  63   58      �� 59       6   �� *	IsChecked+ �� $ FindAncestor=� ToggleButton	 True6�   .a�5:      ��  � ��# �  63   5;      5=      �� 5A   
   " d�6(   .�5B      ��  � ��
 ���� 6+   �� 5C       � ��# �  60   �� 5D       � ��$ Center=�6;   �� 5E       � ��$	 250��6&   �� 5F       � ��$ 40��6'   �� 5G       � �� 5H      �� 5I      "
 d�6&   .R�5J      �� #��z��6!   #��z��6K    6   �� *	Padding+$
 this��._�5K      �  5L   "   �� 5M   &   $��60 �67   �� 5N   &   .+�5R   "   �� 8��  6)   F� 5U   "   $ Uniform=�5V   '   8�� ��5U   '   �� *Icon+$
 this��8  8  ��6�   �� *
IconMargin+$ 0,0,0,0��$ 20,0,0,0����5V   9   �� *	IconWidth+$
 this��$ 20��$ 20��6�   �� 5Y   "   8�� $��Regular"�6S   $��14(�6h   $��1��6v   $��0,0,0,0q�6�   ��6�   �� *Text+$
 this��$ MenuText��$ MenuText��6�   5[      5\       5]      �� 5_      $
	 True��6[    6*   �� *
IsSelected+$
 this��.a�5`   "   ��  �@d�	IsChecked$
 TrueY�6>   5a      5c      5e      5f      5i      .��5k      � .+�5l   
   	� #��[ 6    	� 	GroupName 6?   �� *	GroupName+$
 this��5m      5o      