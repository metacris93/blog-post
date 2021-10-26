export default {
  namespaced: true,
  state: {
    blogType: null,
  },
  mutations: {
    SET_BLOG_TYPE(state, type) {
      state.blogType = type;
    },
  },
  getters: {
    getBlogType(state) {
      return state.blogType;
    },
  },
};
