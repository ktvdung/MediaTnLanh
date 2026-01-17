<template>
  <div class="quiz">
    <h1>Đạo Kinh Thánh - Quiz</h1>
    
    <ErrorAlert :message="store.error" dismissible @dismiss="store.error = null" />
    
    <div class="filters">
      <select v-model="selectedLoai" @change="handleLoaiChange" class="filter-select">
        <option value="">Chọn loại</option>
        <option v-for="l in store.loais" :key="l._id" :value="l._id">{{ l.TenLoai }}</option>
      </select>
      <select v-model="selectedChuDe" @change="handleFilter" class="filter-select">
        <option value="">Chọn chủ đề</option>
        <option v-for="cd in store.chuDes" :key="cd._id" :value="cd._id">{{ cd.TenChuDe }}</option>
      </select>
    </div>

    <LoadingSpinner v-if="store.loading" message="Đang tải..." />
    
    <div v-else-if="store.cauHois.length === 0" class="empty-state">
      <p>Chọn loại và chủ đề để xem câu hỏi</p>
    </div>
    
    <div v-else class="questions">
      <div v-for="(q, index) in store.cauHois" :key="q._id" class="question-card">
        <div class="question-header">
          <span class="question-number">Câu {{ index + 1 }}</span>
        </div>
        <p class="question-text">{{ q.NoiDung }}</p>
        <div class="answers">
          <div 
            v-for="opt in ['A', 'B', 'C', 'D']" 
            :key="opt" 
            class="answer-option"
            :class="{ 
              'correct': showAnswers[q._id] && q.DapAnDung === opt,
              'selected': selectedAnswers[q._id] === opt
            }"
            @click="selectAnswer(q._id, opt)"
          >
            <span class="option-letter">{{ opt }}.</span>
            <span>{{ q[`DapAn${opt}` as keyof typeof q] }}</span>
          </div>
        </div>
        <button 
          v-if="selectedAnswers[q._id] && !showAnswers[q._id]" 
          class="check-btn"
          @click="checkAnswer(q._id)"
        >
          Kiểm tra
        </button>
        <div v-if="showAnswers[q._id]" class="result">
          <p :class="selectedAnswers[q._id] === q.DapAnDung ? 'correct-text' : 'wrong-text'">
            {{ selectedAnswers[q._id] === q.DapAnDung ? '✓ Chính xác!' : '✗ Sai rồi!' }}
          </p>
          <p v-if="q.GiaiThich" class="explanation">{{ q.GiaiThich }}</p>
        </div>
      </div>
    </div>

    <Pagination :currentPage="store.page" :totalPages="store.totalPages" @change="handlePageChange" />
  </div>
</template>

<script setup lang="ts">
import { ref, onMounted, reactive } from 'vue';
import { useQuizStore } from '../stores/quiz';
import ErrorAlert from '../components/ErrorAlert.vue';
import LoadingSpinner from '../components/LoadingSpinner.vue';
import Pagination from '../components/Pagination.vue';

const store = useQuizStore();
const selectedLoai = ref('');
const selectedChuDe = ref('');
const selectedAnswers = reactive<Record<string, string>>({});
const showAnswers = reactive<Record<string, boolean>>({});

onMounted(() => {
  store.fetchLoais();
});

function handleLoaiChange() {
  selectedChuDe.value = '';
  store.fetchChuDes(selectedLoai.value);
}

function handleFilter() {
  if (selectedChuDe.value) {
    store.fetchCauHois({ chuDeId: selectedChuDe.value, page: 1 });
    // Reset answers
    Object.keys(selectedAnswers).forEach(k => delete selectedAnswers[k]);
    Object.keys(showAnswers).forEach(k => delete showAnswers[k]);
  }
}

function handlePageChange(page: number) {
  store.fetchCauHois({ chuDeId: selectedChuDe.value, page });
}

function selectAnswer(questionId: string, answer: string) {
  if (!showAnswers[questionId]) {
    selectedAnswers[questionId] = answer;
  }
}

function checkAnswer(questionId: string) {
  showAnswers[questionId] = true;
}
</script>

<style scoped>
.quiz { padding: 1rem; }
.filters { display: flex; gap: 1rem; margin-bottom: 1.5rem; flex-wrap: wrap; }
.filter-select { padding: 0.75rem; border: 1px solid #d1d5db; border-radius: 6px; min-width: 200px; }
.questions { display: flex; flex-direction: column; gap: 1.5rem; }
.question-card { padding: 1.5rem; border: 1px solid #e5e7eb; border-radius: 12px; background: white; }
.question-header { margin-bottom: 0.75rem; }
.question-number { font-weight: bold; color: #3b82f6; }
.question-text { font-size: 1.1rem; margin-bottom: 1rem; line-height: 1.5; }
.answers { display: flex; flex-direction: column; gap: 0.5rem; }
.answer-option { display: flex; gap: 0.5rem; padding: 0.75rem 1rem; border: 1px solid #e5e7eb; border-radius: 8px; cursor: pointer; transition: all 0.2s; }
.answer-option:hover { border-color: #3b82f6; background: #f0f9ff; }
.answer-option.selected { border-color: #3b82f6; background: #dbeafe; }
.answer-option.correct { border-color: #22c55e; background: #dcfce7; }
.option-letter { font-weight: bold; color: #6b7280; }
.check-btn { margin-top: 1rem; padding: 0.75rem 1.5rem; background: #3b82f6; color: white; border: none; border-radius: 6px; cursor: pointer; }
.check-btn:hover { background: #2563eb; }
.result { margin-top: 1rem; padding: 1rem; background: #f9fafb; border-radius: 8px; }
.correct-text { color: #22c55e; font-weight: bold; }
.wrong-text { color: #ef4444; font-weight: bold; }
.explanation { margin-top: 0.5rem; color: #6b7280; font-style: italic; }
.empty-state { text-align: center; padding: 3rem; color: #6b7280; }
</style>
