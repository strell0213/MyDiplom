   M S B A M L   `   `   `  ����H  DCommunication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:CommunicationCommunication  5   	       Communication.AddQuestion   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:Communication     5   	   -   5          � Title$  Communication��5      $	��500��6   $	��720��6+    � 
ResizeMode$ NoResize=�67    � SizeToContent$ Manual=�6M    � WindowStartupLocation$ CenterScreen=�6d    � WindowState$ Normal=�6�    � Icon$ 
/logog.png��6�   .��5	      � ���	#FF403582.+�5
   
   �� $��Ваш вопрос��6   $��Center=�6%   $��0,25,0,0q�6B   $	��Top=�6T   $
��Bold"�6l   $��18(�6~   
�������6�   �� 5   
   -   5         ��Name$ QueText��5      $
��Left=�6#   $��	84,64,0,0q�6>    �� TextWrapping$
 Wrap=�6Q   $	��Top=�6e   $	��542��6}   $	��113��6�   
����������	#FF231C4C$
��Bold"�6�   $
��Left=�6�   $	��Top=�6�   �� 5   
   $��Communication:��6   $��Center=�6)   $��	0,193,0,0q�6F   $	��Top=�6Y   $
��Bold"�6q   $��18(�6�   
�������6�   �� 5   
   -   5         ��Name$ YesMainButton��5      $1��+Этот ответ мне подходит��6(   $
��Left=�6J   $��
84,401,0,0q�6e   $	��Top=�6y   $��53��6�   $	��266��6�   
�������$
��Bold"�6�   $��Arial Black)�6�   
���������6�   Z� 	 � 	Resources	 5      
 �� 
TargetType �� Property �� Value��%^   '��       �� 5      "
 ��6   .�5      ��  �@��CornerRadius$ 5��65   5      5      5      �� 5   
   -   5        $ AdminQueButton��5      $6��0Отправить вопрос эксперту��6)   $
��Left=�6M   $��355,401,0,0q�6h   $	��Top=�6}   $��53��6�   $	��271��6�   
�������$
��Bold"�6�   $��Arial Black)�6�   
���������6�   Z� 	 5      ��%^   '��       �� 5      "
 ��6   .�5      ��  �@��CornerRadius$ 5��65   5      5      5      �� 5   
   -   5        $ CommAnsButton��5      $��
Ответ��6(   $
��Left=�68   $��
84,368,0,0q�6S   $	��Top=�6g   $��28��6   $	��542��6�   
�������$
��Bold"�6�   $��Arial Black)�6�   
���������6�   Z� 	 5      ��%^   '��       �� 5      "
 ��6   .�5      ��  �@��CornerRadius$ 5��65   5      5       5!      �� 5#   
   $��84,227,94,121q�6   .��5$   
   �� -   5         ��Name$ CommAns��5$      ���	#FF231C4C
�������$
��Auto=�6x   $
��Auto=�6�   $
��Bold"�6�   $
��Left=�6�   $	��542��6  $��Stretch=�6  $��Stretch=�66  5&      5'      �� 5(   
   -   5        $ 
BackButton��5(      $��
Назад��6%   $
��Left=�65   $	��Top=�6P   $��36��6h   $��96��6t   
�������$
��Bold"�6�   $��Times New Roman)�6�   $��18(�6�   
�������$��10,5,0,0q�6  ��6   Z� 	 5)      ��%^   '��       �� 5*      "
 ��6   .�5+      ��  �@��CornerRadius$ 5��65   5,      5-      5.      51      52      