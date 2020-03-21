from gtts import gTTS


T1_answer = ['3', '보라', '동', '1', '1', '초록', '북서', '1']
T2_answer = ['파란', '2', '남', '4', '3', '빨간', '2', '남동']
T3_answer = ['3', '3', '노란', '남서', '1', '2', '서', '주황']


T_answer = [T1_answer, T2_answer, T3_answer]

deviceOrderList = ['Lever', 'Button', 'Joystick', 'Wheel']

T1_order = [0,1,2,0,3,1,2,3]
T2_order = [1,3,2,3,0,1,0,2]
T3_order = [3,0,1,2,0,3,2,1]

T_order = [T1_order, T2_order, T3_order]


def saveTTS(taskName, contents):
    tts = gTTS(text=contents, lang='ko')
    tts.save(taskName + '.mp3')

def generateLever(value):
    value = str(value)
    return '레버를 %s 단계로 맞추세요' % value
def generateIntro(groupNumber):
    value = str(groupNumber)
    return '안녕하세요.   그룹 %s 강의에 오신 것을 환영합니다.   당신은 4개 기계를 가지고 8가지 순서에 맞춰서 동작하는 방법을 배울 예정입니다.   들리는 소리에 집중하여 주어진 인터페이스를 활용하여 순서를 숙지하세요   ' % groupNumber
def generateOutro(groupNumber):
    value = str(groupNumber)
    return '이상 그룹 %s의 강의를 마칩니다.   사후 설문조사를 진행해주세요   ' % groupNumber
def generateJoystick(value):
    value = str(value)
    return '조이스틱의 방향을 %s쪽으로 맞추세요   ' % value
def generateButton(value):
    value = str(value)
    return '6개의 버튼 중 %s 색 버튼을 누르세요   ' % value
def generateWheel(value):
    value = str(value)
    return '휠을 시계 방향으로 %d 바퀴를 돌리세요   ' % value

saveTTS('T1_intro', generateIntro(1))
saveTTS('T2_intro', generateIntro(2))
saveTTS('T3_intro', generateIntro(3))
print("Done on intro")

saveTTS('T1_ontro', generateOutro(1))
saveTTS('T2_ontro', generateOutro(2))
saveTTS('T3_ontro', generateOutro(3))
print("Done on outro")


for i in range(3):
    for j in range(8):
        fileName = 'T' + str(i+1) + '_' + 'sub' + str(j+1)

        generateFunc = None

        orderIdx = T_order[i][j]

        if(orderIdx == 0):
            generateFunc = generateLever
        elif(orderIdx == 1):
            generateFunc = generateButton
        elif(orderIdx == 2):
            generateFunc = generateJoystick
        else:
            generateFunc = generateLever

        saveTTS(fileName, str(j+1) + '번 순서입니다.   ' + generateFunc(T_answer[i][j]))
    print('Done on Group' + str(i+1))


