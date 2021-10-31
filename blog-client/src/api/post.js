import axios from "axios";
const URL = process.env.VUE_APP_API_REMOTE;

export const getPost = async (id) => {
  try {
    return await axios.get(`${URL}/api/post/${id}`);
  } catch (error) {
    console.error(error);
  }
};

export const getPosts = async () => {
  try {
    return await axios.get(`${URL}/api/post`);
  } catch (error) {
    console.error(error);
  }
};

export const savePost = async (post) => {
  try {
    const res = await axios.post(`${URL}/api/post`, post, {
      headers: { "Content-Type": "application/json" },
    });
    return res.status === 201;
  } catch (error) {
    console.error(error);
  }
  return false;
};

export const updatePost = async (post, newPost) => {
  try {
    const postUpdated = {
      ...post,
      ...newPost,
    };
    const res = await axios.patch(`${URL}/api/post`, postUpdated, {
      headers: { "Content-Type": "application/json" },
    });
    return res.status === 200;
  } catch (error) {
    console.error(error);
  }
  return false;
};

export const deletePost = async (id) => {
  try {
    const res = await axios.delete(`${URL}/api/post/${id}`, id, {
      headers: { "Content-Type": "application/json" },
    });
    return res.status === 200;
  } catch (error) {
    console.error(error);
  }
  return false;
};
