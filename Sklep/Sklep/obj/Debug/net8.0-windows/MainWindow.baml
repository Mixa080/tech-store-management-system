   M S B A M L   `   `   `  ����@  <Sklep, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:Sklep.ControlsSklep.Controls  5   	   clr-namespace:SklepSklep  5   	       Sklep.MainWindow   5   	   %ctlsclr-namespace:Sklep.Controls  5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   %localclr-namespace:Sklep     5   	   -   5        $
��home��5        � Title$  
MainWindow��5	   	    � WindowStyle$
 None=�6   
������  � AllowsTransparency$
 True��5
   "    � WindowStartupLocation$ CenterScreen=�5   	    � 
ResizeMode$ CanResizeWithGrip=�6.   .��5      ��    PrimaryBackgroundColor#���   6    �� CornerRadius$ 8��5      ._�5   
   �  � ColumnDefinitions 5      �� 5      $
��Auto �6#   �� 5      $��* �6#   .+�5      �� $��0��6     SecundaryBackgroundColor#���  5      $ 8 0 0 8��5      ._�5      � -   5         �Name$ GridNav��5      $��60��6(    � RowDefinitions 5      �� 5      $��60 �6(   �� 5      $��* �6(   .+�5      � $��0��6   .+�5       �� $��14 0 0 0q�5!      $��32��5"      $��24��6*   $
��Left=�5#      ��5$      �� 5%   "   	 �� ImageSource$&	  /15712_logo-removebg-preview.png��6-   5(      �� 5*      $��Меню��6      PrimaryTextColor#���  5+       $
��Bold"�5,       $��16(�5-       $��Center=�5.       $
��Left=�5/       $��60 0 0 0q�50       51      �� 54      $��1��6!   $��
-1 40 0 20q�6.   .+�55      �� -   5        
 ��Name$
 btnHome��55   !     PopupButtonStyle#���  62   .��56         Sklep.Controls.MenuItem     	GroupName$ MenuItem��6,     PrimaryBlueColor   IndicatorBrush# �  6A     home   Icon# �  6u      	IconWidth$ 16��6�      Text$ Головна��6�   $��Center=�6�   57      �� 5:      -   5        $
 btnOrderList��5:   !   #���  67   .��5;        $ MenuItem��6,   # �  6A    	 email# �  6u   $ 16��6�   $ Замовлення��6�   $��Center=�6�   5<      �� 5?      -   5        $
 btnSecurity��5?   !   #���  66   .��5@        $ MenuItem��6,   # �  6A     lock# �  6u   $ 16��6�   $ 
Вихід��6�   $��Center=�6�   5A      5C      � 5G      -   5         �Name$ Popup��5G       � AllowsTransparency$
 True��6,    � HorizontalOffset$ 0��6G    � VerticalOffset$ 10��6\   .��5H         Sklep.Controls.UserPopup  -   5          Name$ Header��5H   )   #���  69   5I      5L      5M      � 5O      $��1��6    5P      �� 5Q      $��60 �6$   �� 5R      $��* �6$   .+�5U      d� -   5         d�Name$ Tg_Btn��5U      $��1��6/   $��Center=�6?     iToggleButtonStyle#���  6Z   $
��Left=�6�   $��0q�6�   
������ $��
10,15,0,15q�6�   $��30��6�   $��30��6�    � Triggers 5V      :� 5W       :� RoutedEvent$ ToggleButton.Unchecked��6'   .[�5X      �� .��5Y   "   �� -	   5         ��Name$ HideStackPanel��5Y   -   .��5Z   &   W�  �� 
TargetName$ GridNav��66    �� TargetProperty$ Width�6V    g� 	BeginTime	 0:0:06x    W� From 2106�   	 W� To 606�    g� Duration$ 0:0:0.5D�6�   5[   '   5\   #   5]      5^      :� 5_      $ ToggleButton.Checked��6'   .[�5`      �� .��5a   "   �� -
   5        $ ShowStackPanel��5a   -   .��5b   &   W� $ GridNav��66   $ Width�6V   	 0:0:06x    606�    2106�   $ 0:0:0.5D�6�   5c   '   5d   #   5e      5f      5h      �� 5k      $��0��6   $��Right=�6*   $	��Top=�6F   $��0,5,10,0q�6^   $��30��6p    �� Orientation$ 
Horizontal=�6|   $��RightToLeft=�6�   $��2��6�   .+�5l      �� -   5        $
 btnClose��5l       	 IconButtonsStyle#��[	 6/    	
 close#��� 
 6Y   
  � Tag$  IsCloseButton��6�   �� 5m      -   5        $
 
btnRestore��5m      $	��5,0q�61   ��5n      �� 5o      ! �� 
TargetType"! ��6$   " �� BasedOn#" [	 6A   .�5p   "   �� # �� Property# � ��  maximize$ �� Value#$ �  6<   % �� Triggers% 5q   "   �� 5r   &   & �� Binding& 62   �� ' �� ElementName$
' home��( �� Path$( WindowState�) �� Value) 	Maximized6i   .a�5s   *   �� # � ��  restore#$ �  6D   5t   '   5v      5x      �� 5y      -   5        $
 btnMinimize��5y      #��� 	 62     minimize#���  6]   6�   5z       � 5}      -   5        $��
fContainer��5~      $��1��5}      $��20q�5      *  � NavigationUIVisibility$* Hidden=�5�      5�      5�      5�      5�      5�      