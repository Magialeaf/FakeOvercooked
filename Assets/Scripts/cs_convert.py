import os
import codecs
def detect_and_convert(file_path, target_encoding='utf-8-sig'):
    detected_encoding = None
    try:
        # 尝试以GB2312读取
        with codecs.open(file_path, 'r', encoding='gb2312') as f:
            f.read()  # 仅尝试读取，成功则说明是GB2312
            detected_encoding = 'gb2312'
    except UnicodeDecodeError:
        # 如果GB2312失败，再尝试UTF-8-SIG
        try:
            with codecs.open(file_path, 'r', encoding='utf-8-sig') as f:
                f.read()
                detected_encoding = 'utf-8-sig'
        except UnicodeDecodeError:
            print(f"Cannot determine encoding for '{file_path}', skipping.")
            return

    # 如果检测到了编码且与目标编码不同，则进行转换
    if detected_encoding and detected_encoding != target_encoding:
        try:
            with codecs.open(file_path, 'r', encoding=detected_encoding) as f:
                content = f.read()
            with codecs.open(file_path, 'w', encoding=target_encoding) as f:
                f.write(content)
            print(f"Converted '{file_path}' from {detected_encoding} to {target_encoding}")
        except Exception as e:
            print(f"Error converting '{file_path}': {str(e)}")

def convert_cs_files_to_utf8_with_bom(root_folder):
    for folder_name, _, file_names in os.walk(root_folder):
        for file_name in file_names:
            if file_name.endswith('.cs'):
                file_path = os.path.join(folder_name, file_name)
                try:
                    detect_and_convert(file_path)
                except Exception as e:
                    print(f"Error detecting encoding of '{file_path}': {str(e)}")

if __name__ == "__main__":
    folder_path = '.'  # Change this to the directory you want to start the conversion from
    convert_cs_files_to_utf8_with_bom(folder_path)
