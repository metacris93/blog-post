<template>
  <b-container>
    <div v-for="(post, index) in getAllPosts" :key="index">
      <Post
        :id="post.id"
        :title="post.title"
        :content="post.content"
        :createdAt="post.createdAt"
        :updatedAt="post.updatedAt"
        :image="post.image"
        @reload-posts="reloadPosts"
      />
    </div>
  </b-container>
</template>
<script>
import { mapGetters } from "vuex";
import Post from "./Index";
export default {
  name: "PostList",
  props: {
    posts: {
      type: Array,
      default: () => [],
    },
  },
  components: {
    Post,
  },
  computed: {
    ...mapGetters({
      getPosts: "local/getAllPosts",
    }),
    getAllPosts() {
      return this.posts;//this.getPosts;
    }
  },
  // watch: {
  //   posts: {
  //     deep: true,
  //     immediate: true, 
  //     handler (val, oldVal) {
  //       console.log("new and old val", val, oldVal);
  //     }
  //   }
  // },
  methods: {
    reloadPosts () {
      console.log("reloading");
      console.log(this.getPosts);
      this.posts = this.getPosts;
      console.log(this.posts);
    }
  },
};
</script>
