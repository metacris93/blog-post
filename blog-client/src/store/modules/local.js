import { v4 as uuid } from "uuid";
export default {
  namespaced: true,
  getters: {
    getAllPosts() {
      if (localStorage.getItem("posts")) {
        return JSON.parse(localStorage.getItem("posts"));
      }
      return [];
    },
    getPost(id) {
      if (localStorage.getItem("posts")) {
        const posts = JSON.parse(localStorage.getItem("posts"));
        const post = posts.find((item) => item.id === id);
        if (!post) {
          throw new Error("post no encontrado");
        }
        return post;
      }
      throw new Error("post no encontrado");
    },
  },
  mutations: {
    ADD_POST(state, post) {
      let posts = [];
      if (localStorage.getItem("posts")) {
        posts = JSON.parse(localStorage.getItem("posts"));
        posts.push(post);
        localStorage.posts = JSON.stringify(posts);
      } else {
        posts.push(post);
        localStorage.posts = JSON.stringify(posts);
      }
    },
    DELETE_POST(state, id) {
      if (localStorage.getItem("posts")) {
        let posts = JSON.parse(localStorage.getItem("posts"));
        const index = posts.findIndex((i) => i.id === id);
        if (index === -1) {
          throw new Error("post no encontrado: ", id);
        }
        posts.splice(index, 1);
        localStorage.posts = JSON.stringify(posts);
      }
    },
    UPDATE_POST(state, postUpdated) {
      if (localStorage.getItem("posts")) {
        let posts = JSON.parse(localStorage.getItem("posts"));
        const index = posts.findIndex((i) => i.id === postUpdated.id);
        if (index === -1) {
          throw new Error("post no encontrado: ", postUpdated.id);
        }
        const post = posts[index];
        posts[index] = {
          ...post,
          ...postUpdated,
        };
        localStorage.posts = JSON.stringify(posts);
      }
    },
  },
  actions: {
    addPost({ commit }, post) {
      return new Promise((resolve, reject) => {
        try {
          post.id = uuid();
          commit("ADD_POST", post);
          resolve(true);
        } catch (error) {
          reject(error);
        }
      });
    },
    deletePost({ commit }, id) {
      return new Promise((resolve, reject) => {
        try {
          commit("DELETE_POST", id);
          resolve(true);
        } catch (error) {
          reject(error);
        }
      });
    },
    updatePost({ commit }, post) {
      return new Promise((resolve, reject) => {
        try {
          commit("UPDATE_POST", post);
          resolve(true);
        } catch (error) {
          reject(error);
        }
      });
    },
  },
};
